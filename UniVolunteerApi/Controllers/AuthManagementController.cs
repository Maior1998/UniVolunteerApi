using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using UniVolunteerApi.Configuration;
using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Repositories;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly IUniRepository repository;
        private readonly JwtConfig jwtConfig;
        private readonly TokenValidationParameters tokenValidationParameters;

        public AuthManagementController(
            IUniRepository repository
            , IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParameters)
        {
            this.tokenValidationParameters = tokenValidationParameters;
            jwtConfig = optionsMonitor.CurrentValue;
            this.repository = repository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationDto registeringUser)
        {
            if (ModelState.IsValid)
            {
                Task<User> existingUserTask = repository.GetUserAsync(registeringUser.Login);
                User existingUser = await existingUserTask;
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Success = false,
                        Errors = new()
                        {
                            "User with this login already exists"
                        }
                    });
                }

                string salt = SaltHelper.GenerateSalt();
                string passHash = SaltHelper.GetHash(registeringUser.Password, salt);
                User newUser = new()
                {
                    Login = registeringUser.Login,
                    Salt = salt,
                    PasswordHash = passHash,
                    RegisteredOn = DateTime.Now
                };
                User isCreated = await repository.CreateUserAsync(newUser);
                if (isCreated != null)
                {
                    AuthResult jwt = await GenerateJwt(newUser);
                    return Ok(jwt);
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Success = false,
                        Errors = new() { "Unable to create user" }

                    });
                }

            }

            return BadRequest(new RegistrationResponse()
            {
                Success = false,
                Errors = new()
                {
                    "Invalid payload"
                }
            });

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new LoginResponse()
                {
                    Success = false,
                    Errors = new() { "Invalid payload" }
                });

            const string errorString = "User does not exist or password is wrong";
            User user = await repository.GetUserAsync(loginRequest.Login);
            if (user == null)
            {
                return BadRequest(new LoginResponse()
                {
                    Success = false,
                    Errors = new() { errorString }
                });
            }

            if (!user.CheckPass(loginRequest.Password))
            {
                return BadRequest(new LoginResponse()
                {
                    Success = false,
                    Errors = new() { errorString }
                });
            }


            AuthResult jwtToken = await GenerateJwt(user);
            return Ok(jwtToken);

        }

        private async Task<AuthResult> GenerateJwt(User user)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(nameof(UniVolunteerDbModel.Model.User.Id),user.Id.ToString()),
                    new Claim(nameof(UniVolunteerDbModel.Model.User.Login), user.Login),
                    new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToShortDateString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddMonths(1),
                Token = $"{Guid.NewGuid()}-{Guid.NewGuid()}"
            };

            await repository.AddRefreshTokenAsync(refreshToken);

            return new AuthResult()
            {
                Success = true,
                Token = jwtToken,
                RefreshToken = refreshToken.Token
            };
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new RegistrationResponse()
                {
                    Success = false,
                    Errors = new() { "Invalid payload" }
                });

            AuthResult verificationResult = await VerifyAndGenerateToken(tokenRequest);

            if (verificationResult == null)
                return BadRequest(new RegistrationResponse()
                {
                    Success = false,
                    Errors = new() { "Invalid tokens" }
                });

            return Ok(verificationResult);
        }


        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            try
            {
                //Валидация запроса по формату JWT
                var tokenInVerification = jwtTokenHandler.ValidateToken(
                    tokenRequest.Token,
                    tokenValidationParameters,
                    out SecurityToken validatedToken
                    );

                if (validatedToken is not JwtSecurityToken jwtSecurityToken)
                    return null;

                //Проверка алгоритма шифрования токена
                bool result = jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);

                if (!result)
                    return null;


                //Проверка на то, что у токена не истек срок действия
                long utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                DateTime expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new() { "Token has not yet expired" }
                    };

                //Проверка существования токена в базе
                var storedToken = await repository.GetRefreshTokenAsync(tokenRequest.RefreshToken);
                if (storedToken != null)
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new() { "Token does not exist" }
                    };


                //Проверка того, что токен могли уже использовать
                if (storedToken.IsUsed)
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new() { "Token has been used" }
                    };

                //Проверка на то, что токен могли отозвать
                if (storedToken.IsRevoked)
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new() { "Token is revoked" }
                    };


                //Проверка соответствия Jti и JwtId из базы
                string jti = tokenInVerification.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new() { "Token doesn't match" }
                    };

                //Пометка старого токена в базе как использованного.
                storedToken.IsUsed = true;
                await repository.UpdateRefreshTokenAsync(storedToken);
                var user = await repository.GetUserAsync(storedToken.UserId);
                //Генерация нового токена и отправка как ответа.
                return await GenerateJwt(user);


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dateTimeVal = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeVal;
        }


    }
}

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

        public AuthManagementController(
            IUniRepository repository
            , IOptionsMonitor<JwtConfig> optionsMonitor)
        {
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
                            "User with this login already exists."
                        }
                    });
                }

                string salt = SaltHelper.GenerateSalt();
                string passHash = SaltHelper.GetHash(registeringUser.Password, salt);
                User newUser = new()
                {
                    Login = registeringUser.Login,
                    Salt = salt,
                    PasswordHash = passHash
                };
                User isCreated = await repository.CreateUserAsync(newUser);
                if (isCreated != null)
                {
                    string jwt = GenerateJwt(newUser);
                    return Ok(new RegistrationResponse()
                    {
                        Success = true,
                        Token = jwt,

                    });
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
        public async Task<ActionResult> Login()
        {
            throw new NotImplementedException();
        }

        private string GenerateJwt(User user)
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
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}

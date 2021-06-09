using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UniVolunteerApi.Controllers;
using Microsoft.Extensions.Options;
using UniVolunteerApi.Repositories;
using UniVolunteerApi.Configuration;
using UniVolunteerApi.DTOs.Requests;
using Moq;
using Xunit;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Moq
{
    public class AuthManagementControllerTests
    {
        private readonly AuthManagementController _authManamentController;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        private readonly TestOptionsMonitor<JwtConfig> testOptionsMonitor = new TestOptionsMonitor<JwtConfig>(new JwtConfig() { Secret = "12345678901234567890123456789012" });
        private readonly TokenValidationParameters tokenValidationParameters = new TokenValidationParameters();

        public AuthManagementControllerTests()
        {
            _authManamentController = new AuthManagementController(_repository.Object, testOptionsMonitor, tokenValidationParameters);
        }

        [Fact]
        public async Task RegisterTest()
        {
            //Arange
            Guid guid = Guid.NewGuid();
            string fullNameUser = "full name";
            string loginUser = "login";
            string passwordUser = "password";
            User user = new User() {Id = guid, FullName = fullNameUser, Login = loginUser};

            _repository.Setup(x => x.GetUserAsync(guid)).ReturnsAsync(user);
            _repository.Setup(x => x.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            //Act
            UserRegistrationDto userRegistration = new UserRegistrationDto() { FullName = fullNameUser, Login = loginUser, Password = passwordUser};
            ActionResult<AuthResult> register = await _authManamentController.Register(userRegistration); // создает нового пользователя

            //Assert
            Assert.NotNull(register.Result);
            Assert.True(((AuthResult)((OkObjectResult)register.Result).Value).Success);
        }

        [Fact]
        public async Task LoginTest()
        {
            //Arange
            var loginUser = "login";
            var passwordUser = "password";
            var userId = Guid.NewGuid();
            LoginRequest loginRequest = new LoginRequest() { Login = loginUser, Password = passwordUser };
            string salt = SaltHelper.GenerateSalt();
            string hash = SaltHelper.GetHash(passwordUser, salt);

            User user = new User() { Login = loginUser, Id = userId, PasswordHash=hash, Salt=salt};
            

            _repository.Setup(x => x.GetUserAsync(loginUser)).ReturnsAsync(user);

            //Act
            ActionResult login = await _authManamentController.Login(loginRequest);

            //Assert
            Assert.NotNull(login);
            Assert.IsType<OkObjectResult>(login);
        }

       
    }
}

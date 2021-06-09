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
            var guid = Guid.NewGuid();
            var fullNameUser = "full name";
            var loginUser = "login";
            var passwordUser = "password";
            var user = new User() {Id = guid, FullName = fullNameUser, Login = loginUser};

            _repository.Setup(x => x.GetUserAsync(guid)).ReturnsAsync(user);
            _repository.Setup(x => x.CreateUserAsync(user)).ReturnsAsync(user);

            //Act
            var userRegistration = new UserRegistrationDto() { FullName = fullNameUser, Login = loginUser, Password = passwordUser};
            var buf = _authManamentController.Register(userRegistration); // создает нового пользователя

            //Assert
            Assert.NotNull(buf.Result);
        }
    }
}

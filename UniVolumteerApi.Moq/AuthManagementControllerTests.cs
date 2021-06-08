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

namespace UniVolunteerApi.Moq
{
    public class AuthManagementControllerTests
    {
        private readonly AuthManagementController _sut;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        private readonly JwtConfig jwtConfig; 
        private readonly TokenValidationParameters tokenValidationParameters;

        public AuthManagementControllerTests()
        {
            //_sut = new AuthManagementController(_repository.Object, jwtConfig, tokenValidationParameters);
        }

        [Fact]
        public async Task<ActionResult> RegisterTest()
        {
            var guid = Guid.NewGuid();
            var userDto = new UserRegistrationDto() {FullName = "", Login = "", Password = ""};
            var buf = _sut.Register(userDto);
            //var repo = _repository.Setup(x => x.GetUserAsync())

            
            return null;
        }
    }
}

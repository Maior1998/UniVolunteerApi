using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniVolunteerApi.Repositories;
using Moq;
using UniVolunteerApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using UniVolunteerApi.DTOs.Responses;
using Xunit;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Moq
{
    public class UniEventUsersControllerTests
    {
        private readonly UniEventUsersController _usersController;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        public UniEventUsersControllerTests()
        {
            _usersController = new UniEventUsersController(_repository.Object);
        }

        [Fact]
        public async Task GetUserTest()
        {
            //Arange
            string name = "";
            Guid guid = Guid.NewGuid();
            User userDto = new User() { FullName = name, Id = guid };

            _repository.Setup(x => x.GetUserAsync(guid)).ReturnsAsync(userDto);

            //Act
            ActionResult<UserDto> user = await _usersController.GetUser(guid);

            //Assert
            Assert.NotNull(((OkObjectResult)user.Result).Value);
            Assert.Equal(guid, ((UserDto)((OkObjectResult)user.Result).Value).Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniVolunteerApi.Repositories;
using UniVolunteerApi.Services;
using Moq;
using UniVolunteerApi.Controllers;
using Xunit;
using UniVolunteerApi.Model.DTOs.Requests;
using UniVolunteerDbModel.Model;
using Microsoft.AspNetCore.Mvc;
using UniVolunteerApi.Model.DTOs.Responses;
using System.Collections;

namespace UniVolunteerApi.Moq
{
    public class UniUserRolesControllerTests
    {
        private readonly UniUserRolesController _uniUserRolesController;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        private readonly Mock<IUniVolunteerSession> _session = new Mock<IUniVolunteerSession>();

        public UniUserRolesControllerTests()
        {
            _uniUserRolesController = new UniUserRolesController(_repository.Object, _session.Object);
        }

        [Fact]
        public async Task CreateUserRoleTest()
        {
            //Arange
            string nameUserRole = "user role";
            Guid userRoleId = Guid.NewGuid();
            UserRole role = new UserRole() { Id = userRoleId, Name = nameUserRole };
            CreateUserRoleDto createUserRoleDto = new CreateUserRoleDto() { Name = nameUserRole };

            _repository.Setup(x => x.CreateUserRole(It.IsAny<UserRole>())).
                ReturnsAsync(createUserRoleDto.ConvertToUserRole() with { Id = userRoleId});

            //Act
            Task<ActionResult> createUserRole = _uniUserRolesController.CreateUserRole(createUserRoleDto);

            //Assert
            Assert.NotNull(createUserRole.Result);
            Assert.Equal(201, ((CreatedAtActionResult)createUserRole.Result).StatusCode);

        }

        [Fact]
        public async Task GetUserRoleTest()
        {
            //Arange
            Guid roleId = Guid.NewGuid();
            string nameRole = "name role";
            UserRole userRole = new UserRole() { Id = roleId, Name = nameRole };

            _repository.Setup(x => x.GetUserRoleAsync(roleId)).ReturnsAsync(userRole);


            //Act
            ActionResult<UserRoleDto> getUserRole = await _uniUserRolesController.GetUserRole(roleId);

            //Assert
            Assert.NotNull(getUserRole.Result);
            Assert.Equal(nameRole, ((UserRoleDto)((OkObjectResult)getUserRole.Result).Value).Name);
        }

        [Fact]
        public async Task GetUserRolesTest()
        {
            //Arange
            Guid userRoleId1 = Guid.NewGuid();
            Guid userRoleId2 = Guid.NewGuid();
            UserRole[] userRoles = new UserRole[]
            {
                new UserRole(){Id=userRoleId1},
                new UserRole(){Id= userRoleId2}
            };

            _repository.Setup(x => x.GetUserRoles()).ReturnsAsync(userRoles);

            //Act
            ActionResult<UserRoleDto[]> getUserRoles = await _uniUserRolesController.GetUserRoles();

            //Assert
            Assert.NotNull(getUserRoles.Result);
            Assert.Equal(userRoleId1, ((IEnumerable)((OkObjectResult)getUserRoles.Result).Value).Cast<UserRoleDto>().ToArray()[0].Id);
        }

        [Fact]
        public async Task UpdateUserRoleTest()
        {
            //Arange
            Guid roleId = Guid.NewGuid();
            string nameRole = "name role";
            UserRole role = new UserRole() { Id = roleId, Name = nameRole };

            _repository.Setup(x => x.GetUserRoleAsync(roleId)).ReturnsAsync(role);
            UpdateUserRoleDto updateUserRoleDto = new UpdateUserRoleDto() { Name = nameRole };

            //Act
            ActionResult updateUserRole = await _uniUserRolesController.UpdateUserRole(roleId, updateUserRoleDto);

            //Assert
            Assert.NotNull(updateUserRole);
            Assert.Equal(204, ((NoContentResult)updateUserRole).StatusCode);
        }

        [Fact]
        public async Task DeleteUserRoleTest()
        {
            //Arange
            Guid roleId = Guid.NewGuid();
            UserRole role = new UserRole() { Id = roleId };

            _repository.Setup(x => x.GetUserRoleAsync(roleId)).ReturnsAsync(role);

            //Act
            ActionResult deleteUserRole = await _uniUserRolesController.DeleteUserRole(roleId);

            //Assert
            Assert.NotNull(deleteUserRole);
            Assert.IsType<NoContentResult>(deleteUserRole);

        }

        [Fact]
        public async Task EnsureRoleHaveAccessTest()
        {
            //Arange
            var roleId = Guid.NewGuid();
            UserRoleWithAccessDto userRoleWithAccessDto = new UserRoleWithAccessDto() { RoleId = roleId };
            UserRole role = new UserRole() { Id = roleId };
            _repository.Setup(x => x.GetUserRoleAsync(roleId)).ReturnsAsync(role);

            //Act
            ActionResult ensureRoleHaveAccess = await _uniUserRolesController.EnsureRoleHaveAccess(userRoleWithAccessDto);

            //Assert
            Assert.NotNull(ensureRoleHaveAccess);
            Assert.IsType<NoContentResult>(ensureRoleHaveAccess);
        }

        [Fact]
        public async Task EnsureRoleNotHaveAccessTest()
        {
            //Arange
            var roleId = Guid.NewGuid();
            UserRoleWithAccessDto userRoleWithAccessDto = new UserRoleWithAccessDto() { RoleId = roleId };
            UserRole role = new UserRole() { Id = roleId };
            _repository.Setup(x => x.GetUserRoleAsync(roleId)).ReturnsAsync(role);

            //Act
            ActionResult ensureRoleHaveAccess = await _uniUserRolesController.EnsureRoleNotHaveAccess(userRoleWithAccessDto);

            //Assert
            Assert.NotNull(ensureRoleHaveAccess);
            Assert.IsType<NoContentResult>(ensureRoleHaveAccess);
        }
        
        [Fact]
        public async Task SetRoleAccessesTest()
        {
            //Arange
            var roleId = Guid.NewGuid();
            UserRole userRole = new UserRole() { Id = roleId };
            _repository.Setup(x => x.GetUserRoleAsync(roleId)).ReturnsAsync(userRole);

            UserRoleWithAccessDto userRoleWithAccessDto = new UserRoleWithAccessDto() { RoleId = roleId, SecurityAccess = (int)SecurityAccess.CreateEvents };

            //Act
            ActionResult setRoleAccess = await _uniUserRolesController.SetRoleAccesses(userRoleWithAccessDto);

            //Assert
            Assert.IsType<NoContentResult>(setRoleAccess);

        }


    }
}

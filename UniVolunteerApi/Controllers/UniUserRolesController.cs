using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UniVolunteerApi.Model.DTOs.Requests;
using UniVolunteerApi.Model.DTOs.Responses;
using UniVolunteerApi.Repositories;
using UniVolunteerApi.Services;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    /// <summary>
    /// Контроллер, позволяющий получить данные по ролям системы.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UniUserRolesController : Controller
    {
        /// <summary>
        /// Репозиторий данных, которым будет пользоваться данный контроллер
        /// </summary>
        private readonly IUniRepository repository;
        private readonly IUniVolunteerSession session;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера пользователей
        /// при помощи указанного репозитория данных.
        /// </summary>
        /// <param name="repository">Репозиторий данных, который будет использоваться данным контроллером для доступа к данным.</param>
        public UniUserRolesController(IUniRepository repository, IUniVolunteerSession session)
        {
            this.repository = repository;
            this.session = session;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserRole([FromBody] CreateUserRoleDto source)
        {
            UserRole role = source.ConvertToUserRole();
            UserRole createdUserRole = await repository.CreateUserRole(role);
            return CreatedAtAction(
                nameof(GetUserRole),
                new { id = createdUserRole.Id },
                createdUserRole.ConvertToUserRoleDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleDto>> GetUserRole(Guid id)
        {
            UserRole role = await repository.GetUserRoleAsync(id);
            if (role == null)
                return NotFound();
            return Ok(role.ConvertToUserRoleDto());
        }

        [HttpGet]
        public async Task<ActionResult<UserRoleDto[]>> GetUserRoles()
        {
            UserRole[] roles = await repository.GetUserRoles();
            return Ok(roles.Select(x => x.ConvertToUserRoleDto()).ToArray());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserRole(Guid id, [FromBody] UpdateUserRoleDto updateUserRoleDto)
        {
            UserRole role = await repository.GetUserRoleAsync(id);
            if (role == null)
                return NotFound();
            role = role with
            {
                Name = updateUserRoleDto.Name
            };
            await repository.UpdateUserRole(role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserRole(Guid id)
        {
            UserRole role = await repository.GetUserRoleAsync(id);
            if (role == null)
                return NotFound();
            await repository.DeleteUserRole(id);
            return NoContent();
        }

        [Route("RoleRights")]
        [HttpPost]
        public async Task<ActionResult> EnsureRoleHaveAccess([FromBody] UserRoleWithAccessDto addingAcces)
        {
            SecurityAccess access = (SecurityAccess)addingAcces.SecurityAccess;
            UserRole role = await repository.GetUserRoleAsync(addingAcces.RoleId);
            if (role == null)
                return NotFound();
            await repository.EnsureRoleHaveAccess(addingAcces.RoleId, access);
            return NoContent();
        }

        [Route("RoleRights")]
        [HttpDelete]
        public async Task<ActionResult> EnsureRoleNotHaveAccess([FromBody] UserRoleWithAccessDto addingAcces)
        {
            SecurityAccess access = (SecurityAccess)addingAcces.SecurityAccess;
            UserRole role = await repository.GetUserRoleAsync(addingAcces.RoleId);
            if (role == null)
                return NotFound();
            await repository.EnsureRoleNotHaveAccess(addingAcces.RoleId, access);
            return NoContent();
        }

        [Route("RoleRights")]
        [HttpPut]
        public async Task<ActionResult> SetRoleAccesses([FromBody] UserRoleWithAccessDto addingAcces)
        {
            SecurityAccess access = (SecurityAccess) addingAcces.SecurityAccess;
            UserRole role = await repository.GetUserRoleAsync(addingAcces.RoleId);
            if (role == null)
                return NotFound();
            await repository.SetRoleAccesses(addingAcces.RoleId, access);
            return NoContent();
        }
    }
}

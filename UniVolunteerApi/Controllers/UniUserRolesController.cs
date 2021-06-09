using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Создает новую роль пользователя.
        /// </summary>
        /// <param name="source">Объект, содержащий данные, необходимые для создания новой роли.</param>
        /// <returns>Результат выполнения операции.</returns>
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

        /// <summary>
        /// Получает роль по ее Id.
        /// </summary>
        /// <param name="id">Id роли, которую необходимо получить.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleDto>> GetUserRole(Guid id)
        {
            UserRole role = await repository.GetUserRoleAsync(id);
            if (role == null)
                return NotFound();
            return Ok(role.ConvertToUserRoleDto());
        }

        /// <summary>
        /// Получает список всех ролей в системе.
        /// </summary>
        /// <returns>Рузльтат выполнения операции - коллекция всех ролей.</returns>
        [HttpGet]
        public async Task<ActionResult<UserRoleDto[]>> GetUserRoles()
        {
            UserRole[] roles = await repository.GetUserRoles();
            return Ok(roles.Select(x => x.ConvertToUserRoleDto()).ToArray());
        }

        /// <summary>
        /// Изменяет параметры роли.
        /// </summary>
        /// <param name="id">Id роли.</param>
        /// <param name="updateUserRoleDto">Объект, содержащий данные, необходимые для обновления роли.</param>
        /// <returns>Результат выполенния операции.</returns>
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

        /// <summary>
        /// Удаляет роль по ее Id.
        /// </summary>
        /// <param name="id">Id роли, которую необходимо удалить.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserRole(Guid id)
        {
            UserRole role = await repository.GetUserRoleAsync(id);
            if (role == null)
                return NotFound();
            await repository.DeleteUserRole(id);
            return NoContent();
        }

        /// <summary>
        /// Производит проверку что роль имеет указанные права и, если нет, добавляет их ей.
        /// </summary>
        /// <param name="addingAcces">Объект, содержащий номер роли и права (в флаговом формате), которые должны быть переданы роли.</param>
        /// <returns>Результат выполнения операции.</returns>
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

        /// <summary>
        /// Производит проверку что роль не имеет указанные права и, если имеет, отзывает их у нее.
        /// </summary>
        /// <param name="addingAcces">Объект, содержащий номер роли и права (в флаговом формате), которые должны быть отозваны у роли.</param>
        /// <returns>Результат выполнения операции.</returns>
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

        /// <summary>
        /// Устанавливает права для роли.
        /// </summary>
        /// <param name="addingAcces">Объект, содержащий номер роли и права (в флаговом формате), которые нужно установить для роли.</param>
        /// <returns>Результат выполнения операции.</returns>
        [Route("RoleRights")]
        [HttpPut]
        public async Task<ActionResult> SetRoleAccesses([FromBody] UserRoleWithAccessDto addingAcces)
        {
            SecurityAccess access = (SecurityAccess)addingAcces.SecurityAccess;
            UserRole role = await repository.GetUserRoleAsync(addingAcces.RoleId);
            if (role == null)
                return NotFound();
            await repository.SetRoleAccesses(addingAcces.RoleId, access);
            return NoContent();
        }

        /// <summary>
        /// Устанавливает роль пользователю.
        /// </summary>
        /// <param name="request">Объект, содержащий Id пользователя и роли.</param>
        /// <returns>Результат выполнения операции.</returns>
        [Route("UserRole")]
        [HttpPost]
        public async Task<ActionResult> SetRoleToUser([FromBody] SetRoleToUserRequest request)
        {

            User user = await repository.GetUserAsync(request.UserId);
            if (user == null)
                return NotFound();
            UserRole role = await repository.GetUserRoleAsync(request.RoleId);
            if (role == null)
                return NotFound();

            await repository.SetUserRole(request.UserId, request.RoleId);
            return NoContent();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Model.DTOs.Requests;
using UniVolunteerApi.Repositories;
using UniVolunteerApi.Services;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    /// <summary>
    /// Контроллер, позволяющий получить данные по пользователям системы.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UniEventUsersController : Controller
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
        public UniEventUsersController(IUniRepository repository, IUniVolunteerSession session)
        {
            this.repository = repository;
            this.session = session;
        }


        /// <summary>
        /// Получает пользователя по его Id.
        /// </summary>
        /// <param name="id">Id пользователя, информацию по которому необходимо получить.</param>
        /// <returns><see cref="OkObjectResult"/>, содержащий объект, содержащий данные о пользователе, либо <see cref="NotFoundObjectResult"/>, если такой пользователь не найден.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            User user = await repository.GetUserAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user.ConvertToUserDto());
        }

        [HttpPost]
        public async Task<ActionResult> SetRoleToUser([FromBody] SetRoleToUserRequest request)
        {

            User user = await repository.GetUserAsync(request.UserId);
            if (user == null)
                return NotFound();
            UserRole role = await repository.GetUserRoleAsync(request.RoleId);
            if (role == null)
                return NotFound();


            return NoContent();

        }
    }
}

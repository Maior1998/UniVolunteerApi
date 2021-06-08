using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Repositories;
using System.Collections.Generic;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за возможность пользователя записываться и выписываться с различных мероприятий.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EnrollmentController : Controller
    {
        /// <summary>
        /// Id текущего пользователя, вызвавшего данный контроллер
        /// </summary>
        private Guid CurrentUserId => Guid.Parse(User.Claims.Single(x => x.Type == "Id").Value);
        /// <summary>
        /// Репозиторий, который используется данным контроллером для обращения к данным. Заполняется через Service Provider.
        /// </summary>
        private readonly IUniRepository repository;

        /// <summary>
        /// Инициализирует новый экземпляр данного контроллера при помощи объекта, реализующего <see cref="IUniRepository"/>.
        /// </summary>
        /// <param name="repository">Экземпляр репозитория, который будет использоваться данных котроллером.</param>
        public EnrollmentController(IUniRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Получает список всех мероприятий, в которых участвует текущий пользователь.
        /// </summary>
        /// <returns>Коллекция мероприятий, в которых участвует текущий пользователь.</returns>
        [HttpGet]
        public async Task<ActionResult<UniEventDto[]>> GetEnrolledInEvents()
        {
            IEnumerable<UniEvent> events = await repository.GetUserParticipatedInEvents(CurrentUserId);
            return Ok(events.Select(x => x.ConvertToUniEventDto()).ToArray());
        }


        /// <summary>
        /// Записывает пользователя на указанное мероприятие. Если запись уже была произведена, ничего не изменится.
        /// </summary>
        /// <param name="id">Id мероприятия, на которое желает записаться пользователь.</param>
        /// <returns><see cref="NoContentResult"/>, если все прошло успешно, или <see cref="NotFoundObjectResult"/>, если мероприятие с таким Id не найдено.</returns>
        [HttpPost("{id}")]
        public async Task<ActionResult> EnrollIntoEvent(Guid id)
        {
            UniEvent deletingUniEvent = repository.GetEvent(id);
            if (deletingUniEvent == null)
                return NotFound();
            await repository.EnsureUserEnrolledToEvent(CurrentUserId, deletingUniEvent.Id);
            return NoContent();
        }

        /// <summary>
        /// Удаляет пользователя из указанного мероприятия. Если пользователь итак в нем не участвовал, ничего не произойдет.
        /// </summary>
        /// <param name="id">Номер мероприятия, из которого необходимо удалить пользователя.</param>
        /// <returns><see cref="NoContentResult"/>, если все прошло успешно, и <see cref="NotFound"/>, если не будет найдено указанное мероприятие.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> ExitFromEvent(Guid id)
        {
            UniEvent deletingUniEvent = repository.GetEvent(id);
            if (deletingUniEvent == null)
                return NotFound();
            await repository.EnsureUserExitedFromEvent(CurrentUserId, deletingUniEvent.Id);
            return NoContent();
        }
    }
}

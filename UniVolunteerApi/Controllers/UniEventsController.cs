using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Repositories;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    /// <summary>
    /// Представляет собой контроллер для организации событий в университете. Требует аутентификации.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UniEventsController : Controller
    {
        /// <summary>
        /// Репозиторий, который будет использоваться данным контроллером для работы с данными.
        /// </summary>
        private readonly IUniRepository repository;
        /// <summary>
        /// Инициализирует новый контроллер при помощи указанного репозитория.
        /// </summary>
        /// <param name="repository">Репозиторий, который будет использоваться для доступа к данным.</param>
        public UniEventsController(IUniRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Возвращает список всех мероприятий.
        /// </summary>
        /// <returns>Коллекция всех имеющихся мероприятий в системе.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<UniEventDto>> GetUniEvents()
        {
            return Ok(repository.GetAllEvents().Select(x => x.ConvertToUniEventDto()));
        }

        /// <summary>
        /// Получает мероприятие по его Id.
        /// </summary>
        /// <param name="id">Id мероприятия, по которому будет произведен поиск.</param>
        /// <returns><see cref="OkObjectResult"/> с указанным объектом, если поиск произведен успешно, или <see cref="NotFoundObjectResult"/>, если мероприятие не было найдено</returns>
        [HttpGet("{id}")]
        public ActionResult<UniEventDto> GetUniEvent(Guid id)
        {
            UniEvent uniEvent = repository.GetEvent(id);
            if (uniEvent == null)
                return NotFound();
            return Ok(uniEvent.ConvertToUniEventDto());
        }

        /// <summary>
        /// Id пользователя, выполнившего обращение к данному контроллеру.
        /// </summary>
        private Guid CurrentUserId => Guid.Parse(User.Claims.Single(x => x.Type == "Id").Value);

        /// <summary>
        /// Создает новое университетское мероприятие.
        /// </summary>
        /// <param name="source">Объект, содержащий в себе данные, необходимые для создания нового мероприятия.</param>
        /// <returns><see cref="CreatedAtActionResult"/>, указывающий на новый объект и его местонахождение в Api.</returns>
        [HttpPost]
        public ActionResult<UniEventDto> CreateUniEvent(CreateUniEventDto source)
        {
            UniEvent addingEvent = source.ConvertToUniEvent();
            addingEvent.CreatedById = addingEvent.ModifiedById = CurrentUserId;
            addingEvent.Id = Guid.NewGuid();
            addingEvent.CreatedOn = addingEvent.ModifiedOn = DateTime.Now;
            UniEvent createdEvent = repository.CreateUniEvent(addingEvent);
            return CreatedAtAction(
                nameof(GetUniEvent),
                new { id = createdEvent.Id },
                createdEvent.ConvertToUniEventDto());
        }

        /// <summary>
        /// Обновляет (заменяет) указанное мероприятие в системе.
        /// </summary>
        /// <param name="id">Номер мероприятия, которые необходимо обновить.</param>
        /// <param name="source">Объект, содержащий в себе обновленные данные по мероприятию.</param>
        /// <returns><see cref="NoContentResult"/>, если обновление было успешно выполнено, и <see cref="NotFoundObjectResult"/>, если мероприятие не было найдено.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateUniEvent(Guid id, UpdateUniEventDto source)
        {
            UniEvent updatingUniEvent = repository.GetEvent(id);
            if (updatingUniEvent == null)
                return NotFound();

            UniEvent uniEvent = updatingUniEvent with
            {
                Name = source.Name,
                Place = source.Place,
                StartTime = source.StartTime,
                ModifiedOn = DateTime.Now,
                ModifiedById = CurrentUserId
            };
            repository.UpdateUniEvent(uniEvent);
            return NoContent();
        }

        /// <summary>
        /// Удаляет указанное мероприятие из системы.
        /// </summary>
        /// <param name="id">Номер мероприятия, которое необходимо удалить.</param>
        /// <returns><see cref="NoContentResult"/>, если удаление было успешно выполнено, и <see cref="NotFoundObjectResult"/>, если мероприятие не было найдено.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteUniEvent(Guid id)
        {
            UniEvent deletingUniEvent = repository.GetEvent(id);
            if (deletingUniEvent == null)
                return NotFound();
            if (deletingUniEvent.CreatedById != CurrentUserId)
                return Forbid();
            repository.DeleteUniEvent(deletingUniEvent.Id);
            return NoContent();
        }

        

    }
}

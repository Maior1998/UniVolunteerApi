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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UniEventsController : Controller
    {
        private readonly IUniRepository repository;
        public UniEventsController(IUniRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UniEventDto>> GetUniEvents()
        {
            return Ok(repository.GetAllEvents().Select(x => x.ConvertToUniEventDto()));
        }

        [HttpGet("{id}")]
        public ActionResult<UniEventDto> GetUniEvent(Guid id)
        {
            UniEvent uniEvent = repository.GetEvent(id);
            if (uniEvent == null)
                return NotFound();
            return Ok(uniEvent.ConvertToUniEventDto());
        }

        private Guid CurrentUserId => Guid.Parse(User.Claims.Single(x => x.Type == "Id").Value);


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

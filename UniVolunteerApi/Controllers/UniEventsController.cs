using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Repositories;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniEventsController : Controller
    {
        private readonly IUniRepository _repository;

        public UniEventsController(IUniRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UniEventDto>> GetUniEvents()
        {
            return Ok(_repository.GetAllEvents().Select(x => x.ConvertToUniEventDto()));
        }

        [HttpGet("{id}")]
        public ActionResult<UniEventDto> GetUniEvent(Guid id)
        {
            UniEvent uniEvent = _repository.GetEvent(id);
            if (uniEvent == null)
                return NotFound();
            return uniEvent.ConvertToUniEventDto();
        }

        [HttpPost]
        public ActionResult<UniEventDto> CreateUniEvent(CreateUniEventDto source)
        {
            UniEvent addingEvent = source.ConvertToUniEvent();
            addingEvent.Id = Guid.NewGuid();
            addingEvent.CreatedOn = DateTime.Now;
            _repository.CreateUniEvent(addingEvent);
            return CreatedAtAction(
                nameof(GetUniEvent),
                new { id = addingEvent.Id },
                addingEvent.ConvertToUniEventDto());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateUniEvent(Guid id, UpdateUniEventDto source)
        {
            UniEvent updatingUniEvent = _repository.GetEvent(id);
            if (updatingUniEvent == null)
                return NotFound();

            UniEvent uniEvent = updatingUniEvent with
            {
                Name = source.Name,
                Place = source.Place,
                StartTime = source.StartTime,
            };
            _repository.UpdateUniEvent(uniEvent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUniEvent(Guid id)
        {
            UniEvent deletingUniEvent = _repository.GetEvent(id);
            if (deletingUniEvent == null)
                return NotFound();
            _repository.DeleteUniEvent(deletingUniEvent.Id);
            return NoContent();
        }
    }
}

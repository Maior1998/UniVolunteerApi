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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EnrollmentController : Controller
    {
        private Guid CurrentUserId => Guid.Parse(User.Claims.Single(x => x.Type == "Id").Value);
        private readonly IUniRepository repository;
        public EnrollmentController(IUniRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<UniEventDto[]>> GetEnrolledInEvents()
        {
            IEnumerable<UniEvent> events = await repository.GetUserParticipatedInEvents(CurrentUserId);
            return Ok(events.Select(x => x.ConvertToUniEventDto()).ToArray());
        }


        [HttpPost("{id}")]
        public async Task<ActionResult> EnrollIntoEvent(Guid id)
        {
            UniEvent deletingUniEvent = repository.GetEvent(id);
            if (deletingUniEvent == null)
                return NotFound();
            await repository.EnsureUserEnrolledToEvent(CurrentUserId, deletingUniEvent.Id);
            return NoContent();
        }

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

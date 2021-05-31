using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniVolunteerApi.Repositories;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniVolunteerController : Controller
    {
        private readonly IUniVolunteerRepository repository;

        public UniVolunteerController(IUniVolunteerRepository repository)
        {
            this.repository = repository;
        }



        [HttpGet]
        public ActionResult<IEnumerable<UniEvent>> Get()
        {
            return Ok(repository.GetAllEvents());
        }
    }
}

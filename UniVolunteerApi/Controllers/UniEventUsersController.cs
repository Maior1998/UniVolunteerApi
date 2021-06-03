using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Repositories;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniEventUsersController : Controller
    {
        private readonly IUniRepository repository;

        public UniEventUsersController(IUniRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(Guid id)
        {
            User user = repository.GetUser(id);
            if (user == null)
                return NotFound();
            return user.ConvertToUserDto();
        }



    }
}

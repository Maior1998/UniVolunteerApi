using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Repositories;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UniEventUsersController : Controller
    {
        private readonly IUniRepository repository;

        public UniEventUsersController(IUniRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            User user = await repository.GetUserAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user.ConvertToUserDto());
        }





    }
}

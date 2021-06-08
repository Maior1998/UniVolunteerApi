using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVolunteerApi.Repositories;
using UniVolunteerApi.Services;

namespace UniVolunteerApi.Controllers
{
    /// <summary>
    /// Контроллер, позволяющий получить данные по ролям системы.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UniEventRolesController : Controller
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
        public UniEventRolesController(IUniRepository repository, IUniVolunteerSession session)
        {
            this.repository = repository;
            this.session = session;
        }

        [HttpGet]
        public ActionResult<>
    }
}

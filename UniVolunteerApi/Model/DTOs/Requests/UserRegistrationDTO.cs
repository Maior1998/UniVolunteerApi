using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniVolunteerApi.DTOs.Requests
{
    /// <summary>
    /// Представляет собой объект данных запроса регистрации нового пользователя.
    /// </summary>
    public class UserRegistrationDto
    {
        /// <summary>
        /// ФИО создаваемого пользователя.
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    /// <summary>
    /// Представляет собой "объект переноса данных" для запроса создания пользователя.
    /// </summary>
    public record CreateUserDto
    {
        /// <summary>
        /// ФИО пользователя.
        /// </summary>
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [Required]
        public string Login { get; set; }
        /// <summary>
        /// Пароль создаваемого пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        [Required]
        public Guid RoleId { get; set; }
    }
}

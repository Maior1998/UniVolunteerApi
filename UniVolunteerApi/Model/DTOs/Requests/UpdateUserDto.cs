using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    public class UpdateUserDto
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
        /// Роль пользователя в системе.
        /// </summary>
        [Required]
        public Guid RoleId { get; set; }
    }
}

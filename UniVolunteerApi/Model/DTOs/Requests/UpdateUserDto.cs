using System;

namespace UniVolunteerApi.DTOs.Requests
{
    public class UpdateUserDto
    {
        /// <summary>
        /// ФИО пользователя.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        public Guid RoleId { get; set; }
    }
}

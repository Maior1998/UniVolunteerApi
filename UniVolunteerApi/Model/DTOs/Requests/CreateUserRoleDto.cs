using System;
namespace UniVolunteerApi.Model.DTOs.Requests
{
    /// <summary>
    /// Представляет собой "объект переноса данных для запроса создания роли.
    /// </summary>
    public record CreateUserRoleDto
    {
        /// <summary>
        /// Название данной роли.
        /// </summary>
        public string Name { get; set; }
    }
}

using System;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Model.DTOs.Responses
{
    /// <summary>
    /// Объект переноса данных для роли пользователя.
    /// </summary>
    public record UserRoleDto
    {
        /// <summary>
        /// Id записи в базе данных.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Название данной роли.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Права, которыми обладает роль в флаговом формате.
        /// </summary>
        public SecurityAccess Access { get; set; }
    }
}

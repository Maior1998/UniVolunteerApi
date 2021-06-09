using System;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Model.DTOs.Responses
{
    public record UserRoleDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Название данной роли.
        /// </summary>
        public string Name { get; set; }

        public SecurityAccess Access { get; set; }
    }
}

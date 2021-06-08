using System;
namespace UniVolunteerApi.Model.DTOs.Requests
{
    public record CreateUserRoleDto
    {
        /// <summary>
        /// Название данной роли.
        /// </summary>
        public string Name { get; set; }
    }
}

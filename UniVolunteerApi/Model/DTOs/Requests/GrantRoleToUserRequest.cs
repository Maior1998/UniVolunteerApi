using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.Model.DTOs.Requests
{
    /// <summary>
    /// Представляет собой объект запроса на установку роли пользователю.
    /// </summary>
    public record SetRoleToUserRequest
    {
        /// <summary>
        /// Id пользователя, которому необходимо установить роль.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Id устанавливаемой роли.
        /// </summary>
        [Required]
        public Guid RoleId { get; set; }
    }
}

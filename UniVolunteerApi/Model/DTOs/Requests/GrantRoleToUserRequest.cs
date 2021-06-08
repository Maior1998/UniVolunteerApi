using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.Model.DTOs.Requests
{
    public record SetRoleToUserRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}

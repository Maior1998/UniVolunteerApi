using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Model.DTOs.Requests
{
    public record UserRoleWithAccessDto
    {
        [Required]
        public Guid RoleId { get; set; }
        [Required]
        public int SecurityAccess { get; set; }
    }
}

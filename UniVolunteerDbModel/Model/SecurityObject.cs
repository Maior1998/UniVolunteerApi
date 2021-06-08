using System;
using System.Collections.Generic;

namespace UniVolunteerDbModel.Model
{
    public record SecurityObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserRole> RolesWithAccess { get; set; } = new List<UserRole>();
    }
}

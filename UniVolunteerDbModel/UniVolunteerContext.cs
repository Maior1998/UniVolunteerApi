using System;

using Microsoft.EntityFrameworkCore;

using UniVolunteerDbModel.Model;

namespace UniVolunteerDbModel
{
    public class UniVolunteerContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UniEvent> UniEvents { get; set; }
    }
}

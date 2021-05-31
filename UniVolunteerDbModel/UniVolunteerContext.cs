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
        public static bool IsInMemory;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (IsInMemory)
                optionsBuilder.UseInMemoryDatabase("university");
            else
                optionsBuilder.UseSqlite(@"Data Source=./university.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

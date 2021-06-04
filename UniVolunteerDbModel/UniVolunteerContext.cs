using System;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using UniVolunteerDbModel.Model;

namespace UniVolunteerDbModel
{
    public class UniVolunteerContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<User>()
                .HasMany(x => x.ParticipatesInEvents)
                .WithMany(x => x.Participants)
                .UsingEntity(entBuilder => entBuilder.ToTable($"{nameof(User)}_{nameof(UniEvent)}"));

            modelBuilder
                .Entity<User>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UsersInRoles)
                .HasForeignKey(x => x.RoleId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=./university.db");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UniEvent> UniEvents { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

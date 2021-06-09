using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    /// <summary>
    /// Представляет собой репозиторий, работающий на основе ORM EntityFramework.
    /// </summary>
    public class DbContextRepository : IUniRepository
    {
        /// <summary>
        /// Инициирует новый экземпляр данного репозитория и сбрасывает его.
        /// </summary>
        public DbContextRepository()
        {
            Reset();
        }
        public void Reset()
        {
            UniVolunteerContext context = GetContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.UniEvents.Add(new()
            {
                CreatedOn = DateTime.Now,
                Name = "TEST",
                Place = "ASDSADASD",
                StartTime = DateTime.Now.AddDays(3)
            });
            context.SaveChanges();
        }

        private UniVolunteerContext GetContext()
        {
            return new();
        }
        public IEnumerable<UniEvent> GetAllEvents()
        {
            return GetContext().UniEvents.ToArray();
        }

        public UniEvent GetEvent(Guid id)
        {
            return GetContext().UniEvents.SingleOrDefault(x => x.Id == id);
        }

        public UniEvent CreateUniEvent(UniEvent createUniEvent)
        {
            UniVolunteerContext context = GetContext();
            context.UniEvents.Add(createUniEvent);
            context.SaveChanges();
            return createUniEvent;
        }

        public void UpdateUniEvent(UniEvent updatingUniEvent)
        {
            UniVolunteerContext context = GetContext();
            context.UniEvents.Update(updatingUniEvent);
            context.SaveChanges();
        }

        public void DeleteUniEvent(Guid id)
        {
            UniVolunteerContext context = GetContext();
            UniEvent deletingUniEvent = context.UniEvents.Single(x => x.Id == id);
            context.Remove(deletingUniEvent);
            context.SaveChanges();
        }

        public Task<User[]> GetAllUsersAsync()
        {
            UniVolunteerContext context = GetContext();
            return context.Users.ToArrayAsync();
        }

        public Task<User> GetUserAsync(Guid id)
        {
            UniVolunteerContext context = GetContext();
            return context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<User> GetUserAsync(string login)
        {
            UniVolunteerContext context = GetContext();
            return context.Users.SingleOrDefaultAsync(x => x.Login == login);
        }

        public async Task<User> CreateUserAsync(User createUser)
        {
            UniVolunteerContext context = GetContext();
            await context.Users.AddAsync(createUser);
            await context.SaveChangesAsync();
            return createUser;
        }

        public async Task UpdateUserAsync(User updatingUser)
        {
            UniVolunteerContext context = GetContext();
            context.Users.Update(updatingUser);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            UniVolunteerContext context = GetContext();
            Task<User> deletingUser = context.Users.SingleOrDefaultAsync(x => x.Id == id);
            deletingUser.Wait();
            context.Remove(deletingUser.Result);
            await context.SaveChangesAsync();
        }

        public async Task AddRefreshTokenAsync(RefreshToken token)
        {
            UniVolunteerContext context = GetContext();
            await context.RefreshTokens.AddAsync(token);
            await context.SaveChangesAsync();

        }

        public Task<RefreshToken> GetRefreshTokenAsync(string token)
        {
            UniVolunteerContext context = GetContext();
            return context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);
        }

        public Task UpdateRefreshTokenAsync(RefreshToken token)
        {
            UniVolunteerContext context = GetContext();
            context.RefreshTokens.Update(token);
            return context.SaveChangesAsync();
        }

        public async Task EnsureUserEnrolledToEvent(Guid userId, Guid eventId)
        {
            UniVolunteerContext context = GetContext();
            User user = await context.Users.Include(x => x.ParticipatesInEvents).SingleAsync(x => x.Id == userId);
            if (user.ParticipatesInEvents.Any(x => x.Id == eventId)) return;
            UniEvent uniEvent = await context.UniEvents.SingleAsync(x => x.Id == eventId);
            user.ParticipatesInEvents.Add(uniEvent);
            await context.SaveChangesAsync();
        }

        public async Task EnsureUserExitedFromEvent(Guid userId, Guid eventId)
        {
            UniVolunteerContext context = GetContext();
            User user = await context.Users.Include(x => x.ParticipatesInEvents).SingleAsync(x => x.Id == userId);
            UniEvent uniEvent = user.ParticipatesInEvents.SingleOrDefault(x => x.Id == eventId);
            if (uniEvent == null) return;
            user.ParticipatesInEvents.Remove(uniEvent);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<UniEvent>> GetUserParticipatedInEvents(Guid userId)
        {
            UniVolunteerContext context = GetContext();
            IEnumerable<UniEvent> events = await context.UniEvents.Where(x => x.Participants.Any(y=>y.Id == userId)).ToArrayAsync();
            return events;
        }

        public async Task<UserRole> CreateUserRole(UserRole role)
        {
            UniVolunteerContext context = GetContext();
            await context.UserRoles.AddAsync(role);
            await context.SaveChangesAsync();
            return role;
        }

        public async Task DeleteUserRole(Guid id)
        {
            UniVolunteerContext context = GetContext();
            UserRole userRole = await context.UserRoles.SingleAsync(x => x.Id == id);
            context.UserRoles.Remove(userRole);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserRole(UserRole role)
        {
            UniVolunteerContext context = GetContext();
            context.Update(role);
            await context.SaveChangesAsync();
        }

        public Task<UserRole> GetUserRoleAsync(Guid id)
        {
            UniVolunteerContext context = GetContext();
            return context.UserRoles.SingleAsync(x => x.Id == id);
        }

        public Task<UserRole[]> GetUserRoles()
        {
            UniVolunteerContext context = GetContext();
            return context.UserRoles.ToArrayAsync();
        }

        public async Task SetUserRole(Guid userId, Guid roleId)
        {
            UniVolunteerContext context = GetContext();
            User user = await context.Users.SingleAsync(x => x.Id == userId);
            user.RoleId = roleId;
            await context.SaveChangesAsync();
        }

        public async Task SetRoleAccesses(Guid roleId, SecurityAccess newAccess)
        {
            UniVolunteerContext context = GetContext();
            UserRole userRole = await context.UserRoles.SingleAsync(x => x.Id == roleId);
            userRole.Access = newAccess;
            await context.SaveChangesAsync();
        }

        public async Task EnsureRoleHaveAccess(Guid roleId, SecurityAccess access)
        {
            UniVolunteerContext context = GetContext();
            UserRole userRole = await context.UserRoles.SingleAsync(x => x.Id == roleId);
            userRole.Access |= access;
            await context.SaveChangesAsync();
        }

        public async Task EnsureRoleNotHaveAccess(Guid roleId, SecurityAccess access)
        {
            UniVolunteerContext context = GetContext();
            UserRole userRole = await context.UserRoles.SingleAsync(x => x.Id == roleId);
            userRole.Access &= ~(access);
            await context.SaveChangesAsync();
        }

    }
}

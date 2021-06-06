using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public class DbContextRepository : IUniRepository
    {
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
    }
}

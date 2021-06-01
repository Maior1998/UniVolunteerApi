using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public abstract class DbContextRepository : IUniVolunteerRepository
    {
        protected DbContextRepository()
        {
            UniVolunteerContext context = GetContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.UniEvents.Add(new()
            {
                CreatedAt = DateTime.Now,
                Name="TEST",
                Place="ASDSADASD",
                StartTime = DateTime.Now.AddDays(3)
            });
            context.SaveChanges();

        }
        protected abstract UniVolunteerContext GetContext();
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public class InMemoryUniRepository : IUniVolunteerRepository
    {
        static InMemoryUniRepository()
        {
            UniVolunteerContext.IsInMemory = true;
            UniVolunteerContext context = new();
            context.UniEvents.Add(new() { Name = "asdsad", Place = "jkdgndskjg", StartTime = DateTime.Now });
            context.SaveChanges();
        }
        public IEnumerable<UniEvent> GetAllEvents()
        {

            UniVolunteerContext context = new();
            return context.UniEvents.ToArray();
        }
    }
}

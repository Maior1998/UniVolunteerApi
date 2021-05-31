using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public class SqlLiteUniRepository : IUniVolunteerRepository
    {
        static SqlLiteUniRepository()
        {
            UniVolunteerContext.IsInMemory = false;
            var context = new UniVolunteerContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public IEnumerable<UniEvent> GetAllEvents()
        {
            return new UniVolunteerContext().UniEvents.ToArray();
        }
    }
}

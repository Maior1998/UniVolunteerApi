using System;
using System.Collections.Generic;
using System.Linq;

using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public class InMemoryUniRepository : DbContextRepository

    {
        
        protected override UniVolunteerContext GetContext()
        {
            return new InMemoryUniVolunteerContext();
        }
    }
}

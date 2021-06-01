using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public class SqlLiteUniRepository : DbContextRepository
    {
        protected override UniVolunteerContext GetContext()
        {
            return new SqliteUniVolunteerContext();
        }
    }
}

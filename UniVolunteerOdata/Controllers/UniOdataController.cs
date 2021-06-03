using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

using UniVolunteerDbModel;
using UniVolunteerDbModel.Model;

namespace UniVolunteerOdata.Controllers
{
    public class UniOdataController : ODataController
    {
        private readonly UniVolunteerContext context;

        public UniOdataController(UniVolunteerContext context)
        {
            this.context = context;
        }

        [EnableQuery]
        public IEnumerable<UniEvent> GetAllEvents()
        {
            return context.UniEvents.ToArray();
        }
    }
}

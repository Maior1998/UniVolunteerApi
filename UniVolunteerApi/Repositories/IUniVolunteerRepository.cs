using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using UniVolunteerApi.Dtos;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public interface IUniVolunteerRepository
    {
        IEnumerable<UniEvent> GetAllEvents();
        UniEvent CreateUniEvent(CreateUniEventDto createUniEvent);

        void MakeAnAppointment();
    }
}

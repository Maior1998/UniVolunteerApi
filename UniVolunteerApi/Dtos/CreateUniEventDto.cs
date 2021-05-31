using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniVolunteerApi.Dtos
{
    public class CreateUniEventDto
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
    }
}

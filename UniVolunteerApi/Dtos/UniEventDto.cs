using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Dtos
{
    public record UniEventDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }

    }
}

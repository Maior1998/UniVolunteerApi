using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniVolunteerDbModel.Model
{
    public record UserRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniVolunteerDbModel.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public ICollection<UniEvent> ParticipatesInEvents { get; set; } = new List<UniEvent>();
        public UserRole Role { get; set; }
    }
}

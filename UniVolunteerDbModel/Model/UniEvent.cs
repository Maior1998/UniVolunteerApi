using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniVolunteerDbModel.Model
{
    /// <summary>
    /// Представляет собой мероприятие в ВУЗе.
    /// </summary>
    public class UniEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public ICollection<User> Participants { get; set; } = new List<User>();
    }
}

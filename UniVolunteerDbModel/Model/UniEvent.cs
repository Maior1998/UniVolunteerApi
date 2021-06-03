using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniVolunteerDbModel.Model
{
    /// <summary>
    /// Представляет собой мероприятие в ВУЗе.
    /// </summary>
    [Table("UniEvents")]
    public record UniEvent
    {
        /// <summary>
        /// Номер записи в БД.
        /// </summary>

        public Guid Id { get; set; }
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime? StartTime { get; set; }
        public ICollection<User> Participants { get; set; } = new List<User>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniVolunteerDbModel.Model
{
    [Table("UserRoles")]
    public record UserRole
    {
        /// <summary>
        /// Номер записи в БД.
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public ICollection<User> UsersInRoles { get; set; } = new List<User>();
    }
}

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
    /// Представляет собой пользователя системы.
    /// </summary>
    [Table("Users")]
    public record User 
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
        /// <summary>
        /// ФИО пользователя.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Хеш пароля пользователя.
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Соль, применяемая к паролю пользователя.
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        public UserRole Role { get; set; }
        public Guid? RoleId { get; set; }

        /// <summary>
        /// Список мероприятий, в которых данный пользователь принимает участие.
        /// </summary>
        public ICollection<UniEvent> ParticipatesInEvents { get; set; } = new List<UniEvent>();

    }
}

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

        #region Created On\By Properties
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Id пользователя, создавшего данное событие.
        /// </summary>
        public Guid? CreatedById { get; set; }
        /// <summary>
        /// Пользователь, создавший данное событие.
        /// </summary>
        public User CreatedBy { get; set; }
        #endregion

        #region Modified On\By Properties
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// Id пользователя, создавшего данное событие.
        /// </summary>
        public Guid? ModifiedById { get; set; }
        /// <summary>
        /// Пользователь, создавший данное событие.
        /// </summary>
        public User ModifiedBy { get; set; }
        #endregion
        /// <summary>
        /// Название данного события.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Место проведения мероприятия.
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        /// Время проведения мероприятия.
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// Списк участвующих в данном мероприятии пользователей.
        /// </summary>
        public ICollection<User> Participants { get; set; } = new List<User>();
    }
}

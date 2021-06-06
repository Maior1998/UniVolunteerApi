using System;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.DTOs.Responses
{
    /// <summary>
    /// Представляет собой объект переноса данных для сущности события в ВУЗе <see cref="UniEvent"/>.
    /// </summary>
    public record UniEventDto
    {
        /// <summary>
        /// Номер записи в БД.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Id пользователя, создавшего данное событие.
        /// </summary>
        public Guid? CreatedById { get; set; }
        /// <summary>
        /// Время создания записи.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// Id пользователя, создавшего данное событие.
        /// </summary>
        public Guid? ModifiedById { get; set; }
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

    }
}

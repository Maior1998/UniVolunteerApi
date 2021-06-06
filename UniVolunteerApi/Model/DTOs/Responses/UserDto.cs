using System;

namespace UniVolunteerApi.DTOs.Responses
{
    /// <summary>
    /// Представляет собой объект переноса данных для описания сущности пользователя.
    /// </summary>
    public record UserDto
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
        /// ФИО пользователя.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// Время регистрации пользователя.
        /// </summary>
        public DateTime RegisteredOn { get; set; }

    }
}

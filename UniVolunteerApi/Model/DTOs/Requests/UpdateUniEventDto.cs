using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    /// <summary>
    /// Представляет собой объект данных запроса обновления данных мероприятия.
    /// </summary>
    public record UpdateUniEventDto
    {
        /// <summary>
        /// Название мероприятия.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Место проведения мероприятия.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Время начала мероприятия.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }
    }
}

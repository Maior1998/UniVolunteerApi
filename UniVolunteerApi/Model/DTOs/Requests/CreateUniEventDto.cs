using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    /// <summary>
    /// Представляет собой "объект переноса данных"
    /// для представления создания
    /// нового события университета.
    /// </summary>
    public record CreateUniEventDto
    {
        /// <summary>
        /// Название создаваемого мероприятия.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Место проведения мероприятия.
        /// </summary>
        [Required]
        public string Place { get; set; }
        /// <summary>
        /// Время начала мероприятия.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }
    }
}

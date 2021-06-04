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
        [Required]
        public string Name { get; set; }
        public string Place { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    public record UpdateUniEventDto
    {
        [Required]
        public string Name { get; set; }
        public string Place { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
    }
}

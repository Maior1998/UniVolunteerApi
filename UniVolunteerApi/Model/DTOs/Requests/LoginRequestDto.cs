﻿using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    public record LoginRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

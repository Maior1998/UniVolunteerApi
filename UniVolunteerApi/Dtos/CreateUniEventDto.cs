﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniVolunteerApi.Dtos
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

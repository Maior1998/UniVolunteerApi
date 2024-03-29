﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniVolunteerApi.Model.DTOs.Requests
{
    /// <summary>
    /// Представляет собой объект данных запроса обновления данных роли.
    /// </summary>
    public record UpdateUserRoleDto
    {
        /// <summary>
        /// Название данной роли.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}

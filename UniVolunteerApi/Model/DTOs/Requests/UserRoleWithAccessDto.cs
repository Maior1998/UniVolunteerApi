using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Model.DTOs.Requests
{
    /// <summary>
    /// Представляет собой объект данных запроса по парвам пользователя (выдача, отзыв, установка прав).
    /// </summary>
    public record UserRoleWithAccessDto
    {
        /// <summary>
        /// Id роли, у которой необходимо изменить права.
        /// </summary>
        [Required]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Права-аргумент при изменении.
        /// </summary>
        [Required]
        public int SecurityAccess { get; set; }
    }
}

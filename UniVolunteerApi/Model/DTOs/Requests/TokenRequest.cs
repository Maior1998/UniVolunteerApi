using System;
using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    /// <summary>
    /// Представляет собой объект запроса обновления токена доступа пользователя.
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Токен, выданный пользователю ранее (истекший).
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Токен обновления.
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}

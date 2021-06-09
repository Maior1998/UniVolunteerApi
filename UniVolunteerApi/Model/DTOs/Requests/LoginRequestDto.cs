using System.ComponentModel.DataAnnotations;

namespace UniVolunteerApi.DTOs.Requests
{
    /// <summary>
    /// Представляет собой объект запроса на авторизацию пользователя.
    /// </summary>
    public record LoginRequest
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace UniVolunteerApi.DTOs.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

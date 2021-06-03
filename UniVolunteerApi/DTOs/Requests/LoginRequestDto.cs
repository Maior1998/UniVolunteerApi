namespace UniVolunteerApi.DTOs.Requests
{
    public record LoginRequestDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

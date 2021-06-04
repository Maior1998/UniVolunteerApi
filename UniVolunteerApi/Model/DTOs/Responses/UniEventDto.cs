using System;

namespace UniVolunteerApi.DTOs.Responses
{
    public record UniEventDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime? StartTime { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVolunteerDbModel.Model
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ExpiryTime { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}

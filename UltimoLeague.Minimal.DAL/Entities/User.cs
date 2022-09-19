using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("users")]
    public class User: BaseEntity
    {
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool AdminUser { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set;}
        public string? ResetToken { get; set; }
        public DateTime? ResetExpiry { get; set; }
    }
}

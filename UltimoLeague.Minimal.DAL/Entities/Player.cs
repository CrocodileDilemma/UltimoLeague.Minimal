using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("players")]
    public class Player : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public string MembershipNumber { get; set; }
        public TeamMinimal ActiveTeam { get; set; }
    }

    public class PlayerMinimal : BaseMinimalEntity
    {
        public string PlayerName { get; set; }
    }
}

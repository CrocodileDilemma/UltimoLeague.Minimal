using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("registrations")]
    public class Registration : BaseEntity
    {
        public PlayerMinimal Player { get; set; }
        public TeamMinimal Team { get; set; }
        public TeamMinimal PreviousTeam { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}

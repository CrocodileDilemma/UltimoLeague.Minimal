using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("registrations")]
    public class Registration : BaseEntity
    {
        public ObjectId PlayerId { get; set; }
        public ObjectId TeamId { get; set; }
        public ObjectId PreviousTeamId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}

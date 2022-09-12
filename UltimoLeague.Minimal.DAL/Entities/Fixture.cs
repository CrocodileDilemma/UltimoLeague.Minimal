using MongoDB.Bson;
using MongoDB.Driver;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("fixtures")]
    public class Fixture : BaseEntity
    {
        public ObjectId TeamId { get; set; }
        public ObjectId TeamOppId { get; set; }
        public ObjectId ArenaId { get; set; }
        public ObjectId SeasonId { get; set; }
        public ObjectId LeagueId { get; set; }
        public DateTime FixtureDateTime { get; set; }
        public FixtureStatus Status { get; set; }
    }
}

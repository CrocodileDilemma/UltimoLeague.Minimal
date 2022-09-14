using MongoDB.Bson;
using MongoDB.Driver;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("fixtures")]
    public class Fixture : BaseEntity
    {
        public TeamMinimal Team { get; set; }
        public TeamMinimal TeamOpposition { get; set; }
        public Arena Arena { get; set; }
        public ObjectId SeasonId { get; set; }
        public LeagueMinimal League { get; set; }
        public DateTime FixtureDateTime { get; set; }
        public FixtureStatus Status { get; set; }
    }
}

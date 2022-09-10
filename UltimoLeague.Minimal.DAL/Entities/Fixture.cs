using MongoDB.Driver;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("fixtures")]
    public class Fixture : BaseEntity
    {
        public MongoDBRef TeamRef { get; set; }
        public MongoDBRef TeamOppRef { get; set; }
        public MongoDBRef ArenaRef { get; set; }
        public MongoDBRef SeasonRef { get; set; }
        public DateTime FixtureDateTime { get; set; }
        public bool Completed { get; set; }
    }
}

using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("fixtureresults")]
    public class FixtureResult : BaseEntity
    {
        public ObjectId FixtureId { get; set; }
        public ObjectId TeamId { get; set; }
        public int Score { get; set; }
        public FixtureResultStatus Result { get; set; }
    }
}

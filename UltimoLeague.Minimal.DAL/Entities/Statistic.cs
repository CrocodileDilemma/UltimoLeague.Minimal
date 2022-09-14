using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("fixturestatistics")]
    public class Statistic : BaseEntity
    {
        public ObjectId FixtureResultId { get; set; }
        public PlayerMinimal Player { get; set; }
        public int Score { get; set; }
        public DateTime StatisticDateTime { get; set; }
        public StatisticEvent StatisticEvent { get; set; }
    }
}

using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("seasons")]
    public class Season : BaseEntity
    {
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public ObjectId LeagueId{ get; set;}
        public int NoOfMatches { get; set; }
        public ObjectId WinnerId { get; set; }
        public ObjectId RunnerUpId { get; set; }
        public ObjectId MVPId { get; set; }
    }
}

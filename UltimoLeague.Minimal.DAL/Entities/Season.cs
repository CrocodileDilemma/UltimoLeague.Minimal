using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("seasons")]
    public class Season : BaseEntity
    {
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public LeagueMinimal League { get; set;}
        public int NoOfMatches { get; set; }
        public TeamMinimal WinningTeam { get; set; }
        public TeamMinimal RunnerUpTeam { get; set; }
        public PlayerMinimal MVP { get; set; }
    }
}

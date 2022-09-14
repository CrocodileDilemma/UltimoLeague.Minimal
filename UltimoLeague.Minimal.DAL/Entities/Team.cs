using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("teams")]
    public class Team : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public LeagueMinimal League { get; set; }
        public SportMinimal Sport { get; set; }
    }

    public class TeamMinimal : BaseMinimalEntity
    {
        public string Code { get; set; }
    }
}

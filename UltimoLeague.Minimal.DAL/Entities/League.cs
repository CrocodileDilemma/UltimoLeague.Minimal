using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("leagues")]
    public class League : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Gender Gender { get; set; }
        public SportMinimal Sport { get; set; }
    }

    public class LeagueMinimal : BaseMinimalEntity
    {
        public string Code { get; set; }
    }
}
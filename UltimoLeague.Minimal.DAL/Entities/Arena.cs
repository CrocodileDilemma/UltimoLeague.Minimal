using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{

    [BsonCollection("arenas")]
    public class Arena : BaseEntity
    {
        public string ArenaName { get; set; }
    }
}

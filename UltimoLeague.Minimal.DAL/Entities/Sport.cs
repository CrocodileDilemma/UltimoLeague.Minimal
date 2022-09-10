using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.DAL.Entities
{
    [BsonCollection("sports")]
    public class Sport : BaseEntity
    {
        public string SportName { get; set; }
    }
}

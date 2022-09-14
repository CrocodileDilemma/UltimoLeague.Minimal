using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Interfaces;

namespace UltimoLeague.Minimal.DAL.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }

    public abstract class BaseMinimalEntity : IBaseMinimalEntity
    {
        public ObjectId BaseId { get; set; }
    }
}

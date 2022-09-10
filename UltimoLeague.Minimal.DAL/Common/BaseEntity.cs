using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Interfaces;

namespace UltimoLeague.Minimal.DAL.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }
}

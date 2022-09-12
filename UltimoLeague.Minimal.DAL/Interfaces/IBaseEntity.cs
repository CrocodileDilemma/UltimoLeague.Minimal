using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UltimoLeague.Minimal.DAL.Interfaces
{
    public interface IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}

using MongoDB.Bson;
using System.Runtime.CompilerServices;
using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Mapping
{
    public static class IdMapper
    {
        public static ObjectId ToObjectId(this string id)
        {
            return new ObjectId(id);
        }
    }
}

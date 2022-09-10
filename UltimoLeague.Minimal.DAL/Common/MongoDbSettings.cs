using UltimoLeague.Minimal.DAL.Interfaces;

namespace UltimoLeague.Minimal.DAL.Common
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
    }
}

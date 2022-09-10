using MongoDB.Driver;
using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.DAL.Interfaces
{
    public interface IDbConnection
    {
        MongoClient Client { get; }
        IMongoCollection<Arena> Arenas { get; }
        IMongoCollection<FixtureResult> FixtureResults { get; }
        IMongoCollection<Fixture> Fixtures { get; }
        IMongoCollection<Statistic> Statistics { get; }
        IMongoCollection<League> Leagues { get; }
        IMongoCollection<Player> Players { get; }
        IMongoCollection<Registration> Registrations { get; }
        IMongoCollection<Season> Seasons { get; }
        IMongoCollection<Sport> Sports { get; }
        IMongoCollection<Team> Teams { get; }
    }
}
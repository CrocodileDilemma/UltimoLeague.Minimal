namespace UltimoLeague.Minimal.DAL.Interfaces
{
    public interface IMongoDbSettings
    {
        string DbName { get; set; }
        string ConnectionString { get; set; }
    }
}
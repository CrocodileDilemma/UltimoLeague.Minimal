namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IStatisticService
    {
        IEnumerable<StatisticDto> GetByFixtureId(string id);
        Result<StatisticDto> GetById(string id);
        IEnumerable<StatisticDto> GetByPlayerId(string id);
        Task<Result<StatisticDto>> Post(StatisticRequest request);
    }
}
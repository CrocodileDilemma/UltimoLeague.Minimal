namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IFixtureResultService
    {
        Task<Result<IEnumerable<FixtureResultDto>>> Post(FixtureResultRequest request);
    }
}
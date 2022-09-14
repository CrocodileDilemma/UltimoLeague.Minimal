namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<Result<SeasonBaseDto>> Post(SeasonRequest request);
    }
}
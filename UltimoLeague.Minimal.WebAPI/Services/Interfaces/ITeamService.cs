namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface ITeamService
    {
        IEnumerable<TeamDto> GetAll();
        Result<TeamDto> GetById(string id);
        IEnumerable<TeamDto> GetByLeagueId(string id);
        Task<Result<TeamDto>> Post(TeamRequest request);
        Task<Result<TeamDto>> Update(TeamUpdateRequest request);
    }
}

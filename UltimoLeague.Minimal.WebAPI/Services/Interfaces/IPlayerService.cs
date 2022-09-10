namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        Result<PlayerDto> GetById(string id);
        Result<PlayerDto> GetByMembershipNo(string membershipNo);
        IEnumerable<PlayerDto> GetByTeamId(string id);
        Task<Result<PlayerMinimalDto>> Post(PlayerRequest request);
        Task<Result<PlayerMinimalDto>> Update(PlayerUpdateRequest request);
    }
}
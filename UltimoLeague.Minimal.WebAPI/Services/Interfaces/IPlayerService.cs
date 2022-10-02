using System.Threading.Tasks;

namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<Result<PlayerDto>> GetById(string id);
        Task<Result<PlayerDto>> GetByMembershipNo(string membershipNo);
        Task<Result<PlayerDto>> GetByEmailAddress(string emailAddress);
        IEnumerable<PlayerDto> GetByTeamId(string id);
        Task<Result<PlayerDto>> GetByUserId(string id);
        Task<Result<PlayerDto>> Post(PlayerRequest request);
        Task<Result<PlayerDto>> Update(PlayerUpdateRequest request);
    }
}
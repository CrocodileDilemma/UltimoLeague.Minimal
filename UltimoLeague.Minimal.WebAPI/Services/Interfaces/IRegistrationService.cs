using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IRegistrationService
    {
        Result<RegistrationDto> GetById(string id);
        Result<IEnumerable<RegistrationDto>> GetByPlayerId(string id);
        Result<IEnumerable<RegistrationDto>> GetByTeamId(string id);
        Task<Result<Registration>> Post(RegistrationRequest request);
    }
}

namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IRegistrationService
    {
        Result<RegistrationDto> GetById(string id);
        Result<IEnumerable<RegistrationDto>> GetByPlayerId(string id);
        Result<IEnumerable<RegistrationDto>> GetByTeamId(string id);
        Task<Result<RegistrationDto>> Post(RegistrationRequest request);
    }
}

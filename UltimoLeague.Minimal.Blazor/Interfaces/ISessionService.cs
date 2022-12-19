using UltimoLeague.Minimal.Contracts.Dtos;

namespace UltimoLeague.Minimal.Blazor.Interfaces;

public interface ISessionService
{
    Task<SportMinimalDto?> GetCurrentSport();
    Task SetCurrentSport(SportMinimalDto sport);
}

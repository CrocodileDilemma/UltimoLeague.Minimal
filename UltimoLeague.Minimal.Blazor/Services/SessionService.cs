using Blazored.LocalStorage;
using UltimoLeague.Minimal.Blazor.Interfaces;
using UltimoLeague.Minimal.Contracts.Dtos;

namespace UltimoLeague.Minimal.Blazor.Services;

public class SessionService : ISessionService
{   
    private readonly ILocalStorageService _localStorage;

    public SessionService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<SportMinimalDto?> GetCurrentSport()
    {
       return await _localStorage.GetItemAsync<SportMinimalDto>("currentSport");
    }

    public async Task SetCurrentSport(SportMinimalDto sport)
    {
        await _localStorage.SetItemAsync("currentSport", sport);
    }
}

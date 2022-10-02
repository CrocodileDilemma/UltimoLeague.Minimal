using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.Contracts.Requests;

namespace UltimoLeague.Minimal.Blazor.Interfaces
{
    public interface IAuthenticationService
    {
        Task<SessionDto> Login(SessionRequest request);
        Task Logout();
        Task<MessageDto> Register(RegisterRequest request);
    }
}
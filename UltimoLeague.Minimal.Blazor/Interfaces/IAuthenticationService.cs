using ErrorOr;
using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.Contracts.Requests;

namespace UltimoLeague.Minimal.Blazor.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ErrorOr<SessionDto>> Login(SessionRequest request);
        Task Logout();
        Task<ErrorOr<MessageDto>> Register(RegisterRequest request);
    }
}
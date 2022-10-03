using ErrorOr;
using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.Contracts.Requests;

namespace UltimoLeague.Minimal.Blazor.Interfaces
{
    public interface IAuthenticationService : IBaseService
    {
        Task<ErrorOr<SessionDto>> Login(SessionRequest request);
        Task Logout();
    }
}
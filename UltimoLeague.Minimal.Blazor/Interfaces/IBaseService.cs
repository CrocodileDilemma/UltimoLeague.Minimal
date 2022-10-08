using ErrorOr;

namespace UltimoLeague.Minimal.Blazor.Interfaces
{
    public interface IBaseService
    {
        Task<ErrorOr<T>> Post<T>(string uri, object request);
        Task<ErrorOr<T>> Get<T>(string uri);
    }
}
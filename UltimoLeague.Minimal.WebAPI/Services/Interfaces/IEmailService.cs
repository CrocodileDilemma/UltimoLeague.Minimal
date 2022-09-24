using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendResetEmail(User user, CancellationToken cancellationToken);
        Task SendVerificationEmail(User user, CancellationToken cancellationToken);
    }
}
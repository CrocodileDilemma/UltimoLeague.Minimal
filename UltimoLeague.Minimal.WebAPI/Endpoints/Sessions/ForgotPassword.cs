using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sessions
{
    [AllowAnonymous]
    [HttpPost("sessions/forgotPassword")]
    public class ForgotPassword : Endpoint<EmailAddressRequest, string>
    {
        private readonly SessionService _service;
        public ForgotPassword(SessionService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(EmailAddressRequest request, CancellationToken ct)
        {
            await _service.ForgotPassword(request);
            await SendOkAsync("An email containing a reset link has been sent.", cancellation: ct);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sessions
{
    [AllowAnonymous]
    [HttpPost("sessions/reset")]
    public class Reset : Endpoint<ResetPasswordRequest, string>
    {
        private readonly SessionService _service;
        public Reset(SessionService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(ResetPasswordRequest request, CancellationToken ct)
        {
            Result<string> result = await _service.ResetPassword(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, cancellation: ct);
        }
    }
}

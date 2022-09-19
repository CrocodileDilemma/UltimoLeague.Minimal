using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sessions
{
    [AllowAnonymous]
    [HttpPost("sessions/verify")]
    public class Verify : Endpoint<VerificationRequest, string>
    {
        private readonly SessionService _service;
        public Verify(SessionService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(VerificationRequest request, CancellationToken ct)
        {
            Result<string> result = await _service.Verify(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, cancellation: ct);
        }
    }
}

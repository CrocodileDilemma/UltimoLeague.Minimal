using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.Contracts.Requests;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sessions
{
    [AllowAnonymous]
    [HttpPost("sessions/logon")]    
    public class Logon : Endpoint<SessionRequest, SessionDto>
    {
        private readonly SessionService _service;
        public Logon(SessionService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(SessionRequest request, CancellationToken ct)
        {
            Result<SessionDto> result = await _service.Logon(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendAsync(result.Value, cancellation: ct);
        }
    }
}

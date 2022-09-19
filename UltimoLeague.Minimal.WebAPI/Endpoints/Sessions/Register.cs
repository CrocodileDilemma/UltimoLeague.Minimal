using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sessions
{
    [AllowAnonymous]
    [HttpPost("sessions/register")]
    public class Register : Endpoint<SessionRequest, SessionDto>
    {
        private readonly SessionService _service;
        public Register(SessionService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(SessionRequest request, CancellationToken ct)
        {
            Result<User> result = await _service.Register(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendAsync(result.Value.Adapt<SessionDto>(), cancellation: ct);
        }
    }
}

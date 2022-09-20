using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Users
{
    [AllowAnonymous]
    [HttpPost("users/logon")]    
    public class Logon : Endpoint<SessionRequest, SessionDto>
    {
        private readonly UserService _service;
        public Logon(UserService service)
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

            await SendOkAsync(result.Value, cancellation: ct);
        }
    }
}

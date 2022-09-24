using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Users
{
    [AllowAnonymous]
    [HttpPost("users/register")]
    public class Register : Endpoint<RegisterRequest, MessageDto>
    {
        private readonly UserService _service;
        public Register(UserService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
        {
            Result<MessageDto> result = await _service.Register(request, ct);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, cancellation: ct);
        }
    }
}

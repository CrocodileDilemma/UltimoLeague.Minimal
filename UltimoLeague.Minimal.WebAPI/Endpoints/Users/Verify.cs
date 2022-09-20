using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Users
{
    [AllowAnonymous]
    [HttpPost("users/verify")]
    public class Verify : Endpoint<VerificationRequest, MessageDto>
    {
        private readonly UserService _service;
        public Verify(UserService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(VerificationRequest request, CancellationToken ct)
        {
            Result<MessageDto> result = await _service.Verify(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, cancellation: ct);
        }
    }
}

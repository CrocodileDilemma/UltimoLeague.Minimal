using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Users
{
    [AllowAnonymous]
    [HttpPost("users/reset")]
    public class Reset : Endpoint<ResetPasswordRequest, MessageDto>
    {
        private readonly UserService _service;
        public Reset(UserService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(ResetPasswordRequest request, CancellationToken ct)
        {
            Result<MessageDto> result = await _service.ResetPassword(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, cancellation: ct);
        }
    }
}

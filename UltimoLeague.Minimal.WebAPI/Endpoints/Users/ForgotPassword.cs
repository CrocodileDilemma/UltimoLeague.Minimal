using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Messages;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Users
{
    [AllowAnonymous]
    [HttpPost("users/forgotPassword")]
    public class ForgotPassword : Endpoint<ForgotPasswordRequest, MessageDto>
    {
        private readonly UserService _service;
        public ForgotPassword(UserService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(ForgotPasswordRequest request, CancellationToken ct)
        {
            await _service.ForgotPassword(request.EmailAddress, ct);
            await SendOkAsync(UserMessages.ForgotPassword, cancellation: ct);
        }
    }
}

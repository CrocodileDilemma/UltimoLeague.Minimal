using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Players
{
    [HttpGet("players/getByEmail/{emailAddress}")]
    public class GetByEmail : Endpoint<EmailAddressRequest, PlayerDto>
    {
        private readonly PlayerService _service;
        public GetByEmail(PlayerService service)
        {
            _service = service;
        }
        public override async Task HandleAsync(EmailAddressRequest request, CancellationToken ct)
        {
            Result<PlayerDto> result = await _service.GetByEmailAddress(request.EmailAddress);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, ct);
        }
    }
}

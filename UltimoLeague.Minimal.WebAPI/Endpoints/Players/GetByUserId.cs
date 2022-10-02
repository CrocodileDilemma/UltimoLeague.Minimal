using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Players
{
    [HttpGet("players/getByUserId/{id}")]
    public class GetByUserId : Endpoint<IdRequest, PlayerDto>
    {
        private readonly PlayerService _service;
        public GetByUserId(PlayerService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<PlayerDto> result = await _service.GetByUserId(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, ct);
        }
    }
}

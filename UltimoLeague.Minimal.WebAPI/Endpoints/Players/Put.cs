using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Players
{
    [HttpPut("players/{id}")]
    public class Put : Endpoint<PlayerUpdateRequest, PlayerMinimalDto>
    {
        private readonly PlayerService _service;
        public Put(PlayerService service)
        {
            _service = service;
        }
        public override async Task HandleAsync(PlayerUpdateRequest request, CancellationToken ct)
        {
            Result<PlayerMinimalDto> result = await _service.Update(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value,
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

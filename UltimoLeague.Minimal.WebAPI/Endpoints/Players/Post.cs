using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Players
{
    [HttpPost("players")]
    public class Post : Endpoint<PlayerRequest, PlayerMinimalDto>
    {
        private readonly PlayerService _service;
        public Post(PlayerService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(PlayerRequest request, CancellationToken ct)
        {
            Result<PlayerMinimalDto> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value,
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

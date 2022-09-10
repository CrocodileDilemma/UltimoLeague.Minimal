using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Seasons
{
    [HttpPost("seasons")]
    public class Post : Endpoint<SeasonRequest, SeasonDto>
    {
        private readonly SeasonService _service;
        public Post(SeasonService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(SeasonRequest request, CancellationToken ct)
        {
            Result<Season> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<SeasonDto>(),
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

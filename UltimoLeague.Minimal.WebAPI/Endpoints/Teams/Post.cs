using UltimoLeague.Minimal.WebAPI.Endpoints.Teams;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoTeam.Minimal.WebAPI.Endpoints.Teams
{
    [HttpPost("teams")]
    public class Post : Endpoint<TeamRequest, TeamDto>
    {
        private readonly TeamService _service;
        public Post(TeamService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(TeamRequest request, CancellationToken ct)
        {
            Result<TeamDto> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value,
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

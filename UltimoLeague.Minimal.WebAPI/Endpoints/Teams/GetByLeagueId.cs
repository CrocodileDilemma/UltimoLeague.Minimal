using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Teams
{
    [HttpGet("teams/getByLeagueId/{id}")]
    public class GetByLeagueId : Endpoint<IdRequest, IEnumerable<TeamDto>>
    {
        private readonly TeamService _service;

        public GetByLeagueId(TeamService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            IEnumerable<TeamDto> result = _service.GetByLeagueId(request.Id);
            await SendOkAsync(result, ct);
        }
    }
}

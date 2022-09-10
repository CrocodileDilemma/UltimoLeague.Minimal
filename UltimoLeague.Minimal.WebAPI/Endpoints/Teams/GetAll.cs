using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Teams
{
    [HttpGet("teams")]
    public class GetAll : EndpointWithoutRequest<IEnumerable<TeamDto>>
    {
        private readonly TeamService _service;
        public GetAll(TeamService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = _service.GetAll();

            await SendOkAsync(result, ct);
        }
    }
}

using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Players
{
    [HttpGet("players/getByTeamId/{id}")]
    public class GetByTeamId : Endpoint<IdRequest, IEnumerable<PlayerDto>>
    {
        private readonly PlayerService _service;

        public GetByTeamId(PlayerService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            IEnumerable<PlayerDto> result = _service.GetByTeamId(request.Id);
            await SendOkAsync(result, ct);
        }
    }
}

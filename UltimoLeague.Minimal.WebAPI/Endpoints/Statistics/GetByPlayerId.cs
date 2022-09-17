using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Statistics
{
    [HttpGet("statistics/getByPlayerId/{id}")]
    public class GetByPlayerId : Endpoint<IdRequest, IEnumerable<StatisticDto>>
    {
        private readonly StatisticService _service;

        public GetByPlayerId(StatisticService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            IEnumerable<StatisticDto> result = _service.GetByPlayerId(request.Id);
            await SendOkAsync(result, ct);
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Statistics
{
    [HttpGet("statistics/getByFixtureId/{id}")]
    public class GetByFixtureId : Endpoint<IdRequest, IEnumerable<StatisticDto>>
    {
        private readonly IBaseService<Statistic> _service;

        public GetByFixtureId(IBaseService<Statistic> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<Statistic>> result = _service.GetByValue(x => x.FixtureResultId == request.Id.ToObjectId());
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<IEnumerable<StatisticDto>>(), ct);
        }
    }
}

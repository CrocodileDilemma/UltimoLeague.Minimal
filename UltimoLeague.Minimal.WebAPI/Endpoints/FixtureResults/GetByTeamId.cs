using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.FixtureResults
{
    [HttpGet("results/getByTeamId/{id}")]
    public class GetByTeamId : Endpoint<IdRequest, IEnumerable<FixtureResultDto>>
    {
        private readonly IBaseService<FixtureResult> _service;

        public GetByTeamId(IBaseService<FixtureResult> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<FixtureResult>> result = _service.GetByValue(x => x.Team.BaseId == request.Id.ToObjectId());
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<IEnumerable<FixtureResultDto>>(), ct);
        }
    }
}

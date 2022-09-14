using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Fixtures
{
    [HttpGet("fixtures/getByTeamId/{id}")]
    public class GetByTeamId : Endpoint<IdRequest, IEnumerable<FixtureDto>>
    {
        private readonly IBaseService<Fixture> _service;

        public GetByTeamId(IBaseService<Fixture> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<Fixture>> result = _service.GetByValue(x => x.Team.BaseId == request.Id.ToObjectId()
                || x.TeamOpposition.BaseId == request.Id.ToObjectId());
            
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<IEnumerable<FixtureDto>>(), ct);
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Fixtures
{
    [HttpGet("fixtures/getBySeasonId/{id}")]
    public class GetBySeasonId : Endpoint<IdRequest, IEnumerable<FixtureDto>>
    {
        private readonly IBaseService<Fixture> _service;

        public GetBySeasonId(IBaseService<Fixture> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<Fixture>> result = _service.GetByValue(x => x.SeasonId == request.Id.ToObjectId());
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<IEnumerable<FixtureDto>>(), ct);
        }
    }
}

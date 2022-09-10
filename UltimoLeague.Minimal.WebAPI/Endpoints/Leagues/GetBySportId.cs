using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Leagues
{
    [HttpGet("leagues/getBySportId/{id}")]
    public class GetBySportId : Endpoint<IdRequest, IEnumerable<LeagueDto>>
    {
        private readonly IBaseService<League> _service;

        public GetBySportId(IBaseService<League> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<League>> result = _service.GetByValue(x => x.Sport.Id == request.Id.ToObjectId());
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<IEnumerable<LeagueDto>>(), ct);
        }
    }
}

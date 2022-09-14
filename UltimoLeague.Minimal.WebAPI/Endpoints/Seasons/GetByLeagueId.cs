using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Seasons
{
    [HttpGet("seasons/getByLeagueId/{id}")]
    public class GetByLeagueId : Endpoint<IdRequest, IEnumerable<SeasonDto>>
    {
        private readonly IBaseService<Season> _service;

        public GetByLeagueId(IBaseService<Season> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<Season>> result = _service.GetByValue(x => x.League.BaseId == request.Id.ToObjectId());
            
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<IEnumerable<SeasonDto>>(), ct);
        }
    }
}

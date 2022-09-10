using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Seasons
{
    [HttpGet("seasons/{id}")]
    public class GetById : Endpoint<IdRequest, SeasonDto>
    {
        private readonly IBaseService<Season> _service;

        public GetById(IBaseService<Season> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<Season> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<SeasonDto>(), ct);
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Seasons
{
    [HttpGet("seasons")]
    public class GetAll : EndpointWithoutRequest<IEnumerable<SeasonDto>>
    {
        private readonly IBaseService<Season> _service;
        public GetAll(IBaseService<Season> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = _service.GetAll().AsEnumerable()
                .Adapt<IEnumerable<SeasonDto>>();

            await SendOkAsync(result, ct);
        }
    }
}

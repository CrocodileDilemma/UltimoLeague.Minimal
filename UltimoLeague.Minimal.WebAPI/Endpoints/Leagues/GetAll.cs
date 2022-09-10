using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Leagues
{
    [HttpGet("leagues")]
    public class GetAll : EndpointWithoutRequest<IEnumerable<LeagueDto>>
    {
        private readonly IBaseService<League> _service;
        
        public GetAll(IBaseService<League> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = _service.GetAll().AsEnumerable()
                .Adapt<IEnumerable<LeagueDto>>();

            await SendOkAsync(result, ct);
        }
    }
}

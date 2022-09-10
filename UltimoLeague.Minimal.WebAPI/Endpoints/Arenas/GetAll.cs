using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Arenas
{
    [HttpGet("arenas")]
    public class GetAll : EndpointWithoutRequest<IEnumerable<ArenaDto>>
    {
        private readonly IBaseService<Arena> _service;

        public GetAll(IBaseService<Arena> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = _service.GetAll().AsEnumerable()
                .Adapt<IEnumerable<ArenaDto>>();

            await SendOkAsync(result, ct);
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Arenas
{
    [HttpGet("arenas/getByName/{name}")]
    public class GetByName : Endpoint<NameRequest, ArenaDto>
    {
        private readonly IBaseService<Arena> _service;

        public GetByName(IBaseService<Arena> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(NameRequest request, CancellationToken ct)
        {
            Result<Arena> result = await _service.GetSingleByValue(x => x.ArenaName == request.Name);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<ArenaDto>(), ct);
        }
    }
}

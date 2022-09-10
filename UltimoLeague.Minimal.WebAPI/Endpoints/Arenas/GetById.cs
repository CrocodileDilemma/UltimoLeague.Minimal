using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Arenas
{
    [HttpGet("arenas/{id}")]
    public class GetById : Endpoint<IdRequest, ArenaDto>
    {
        private readonly IBaseService<Arena> _service;

        public GetById(IBaseService<Arena> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<Arena> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<ArenaDto>(), ct);
        }
    }
}

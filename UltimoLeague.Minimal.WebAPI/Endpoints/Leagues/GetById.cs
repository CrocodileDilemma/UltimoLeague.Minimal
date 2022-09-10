using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Leagues
{
    [HttpGet("leagues/{id}")]
    public class GetById : Endpoint<IdRequest, LeagueDto>
    {
        private readonly IBaseService<League> _service;

        public GetById(IBaseService<League> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<League> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<LeagueDto>(), ct);
        }
    }
}

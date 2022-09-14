using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Fixtures
{
    [HttpGet("fixtures/{id}")]
    public class GetById : Endpoint<IdRequest, FixtureDto>
    {
        private readonly IBaseService<Fixture> _service;
        public GetById(IBaseService<Fixture> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<Fixture> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<FixtureDto>(), ct);
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.FixtureResults
{
    [HttpGet("results/{id}")]
    public class GetById : Endpoint<IdRequest, FixtureResultDto>
    {
        private readonly IBaseService<FixtureResult> _service;

        public GetById(IBaseService<FixtureResult> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<FixtureResult> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<FixtureResultDto>(), ct);
        }
    }
}

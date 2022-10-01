using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.FixtureResults
{
    [HttpPost("results")]
    [Authorize(Policy = Policy.AdminOnly)]
    public class Post : Endpoint<FixtureResultRequest, IEnumerable<FixtureResultDto>>
    {
        private readonly FixtureResultService _service;
        public Post(FixtureResultService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(FixtureResultRequest request, CancellationToken ct)
        {
            Result<IEnumerable<FixtureResultDto>> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetByFixtureId>( new { Id = result.Value.First().FixtureId }, result.Value,
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}
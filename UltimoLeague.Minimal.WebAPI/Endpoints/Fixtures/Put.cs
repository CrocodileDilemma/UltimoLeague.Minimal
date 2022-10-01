using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Fixtures
{
    [HttpPut("fixtures/{id}")]
    [Authorize(Policy = Policy.AdminOnly)]
    public class Put : Endpoint<FixtureUpdateRequest, FixtureDto>
    {
        private readonly FixtureService _service;
        public Put(FixtureService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(FixtureUpdateRequest request, CancellationToken ct)
        {
            Result<Fixture> result = await _service.Update(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<FixtureDto>(),
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

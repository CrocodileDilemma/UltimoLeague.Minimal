using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Statistics
{
    [HttpPost("statistics")]
    [Authorize(Policy = Policy.AdminOnly)]
    public class Post : Endpoint<StatisticRequest, StatisticDto>
    {
        private readonly StatisticService _service;
        public Post(StatisticService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(StatisticRequest request, CancellationToken ct)
        {
            Result<StatisticDto> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value,
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

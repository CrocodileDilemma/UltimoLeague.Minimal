using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Leagues
{
    [HttpPost("leagues")]
    [Authorize(Policy = Policy.AdminOnly)]
    public class Post : Endpoint<LeagueRequest, LeagueDto>
    {
        private readonly LeagueService _service;
        public Post(LeagueService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(LeagueRequest request, CancellationToken ct)
        {
            Result<League> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<LeagueDto>(),
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

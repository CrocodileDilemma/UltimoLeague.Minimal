using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Leagues;

[HttpPut("leagues/{id}")]
[Authorize(Policy = Policy.AdminOnly)]
public class Put : Endpoint<LeagueUpdateRequest, LeagueDto>
{
    private readonly LeagueService _service;
    public Put(LeagueService service)
    {
        _service = service;
    }
    public override async Task HandleAsync(LeagueUpdateRequest request, CancellationToken ct)
    {
        Result<League> result = await _service.Update(request);
        if (result.IsFailed)
        {
            ThrowError(result.Errors[0].Message);
        }

        await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<LeagueDto>(),
            generateAbsoluteUrl: true, cancellation: ct);
    }
}

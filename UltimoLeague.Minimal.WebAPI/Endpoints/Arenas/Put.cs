using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Arenas
{
    [HttpPut("arenas/{id}")]
    [Authorize(Policy = Policy.AdminOnly)]
    public class Put : Endpoint<ArenaDto, ArenaDto>
    {
        private readonly ArenaService _service;
        public Put(ArenaService service)
        {
            _service = service;
        }
        public override async Task HandleAsync(ArenaDto request, CancellationToken ct)
        {
            Result<Arena> result = await _service.Update(request.Adapt<Arena>());
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<ArenaDto>(),
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

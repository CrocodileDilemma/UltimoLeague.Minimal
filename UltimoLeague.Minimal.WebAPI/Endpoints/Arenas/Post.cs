using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Arenas
{
    [HttpPost("arenas")]
    [Authorize(Policy= Policy.AdminOnly)]
    public class Post : Endpoint<ArenaRequest, ArenaDto>
    {
        private readonly ArenaService _service;
        
        public Post(ArenaService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(ArenaRequest request, CancellationToken ct)
        {
            Result<Arena> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<ArenaDto>(),
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

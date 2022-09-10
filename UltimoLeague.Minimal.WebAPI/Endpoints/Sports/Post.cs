using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sports
{
    [HttpPost("sports")]
    public class Post : Endpoint<SportRequest, SportDto>
    {
        private readonly SportService _service;
        public Post(SportService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(SportRequest request, CancellationToken ct)
        {
            Result<Sport> result = await _service.Post(request.SportName);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<SportDto>(), 
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

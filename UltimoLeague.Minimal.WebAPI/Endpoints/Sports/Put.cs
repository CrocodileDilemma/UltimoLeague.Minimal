using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sports
{
    [HttpPut("sports/{id}")]
    public class Put : Endpoint<SportUpdateRequest, SportDto>
    {
        private readonly SportService _service;
        public Put(SportService service)
        {
            _service = service;
        }
        public override async Task HandleAsync(SportUpdateRequest request, CancellationToken ct)
        {
            Result<Sport> result = await _service.Update(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<SportDto>(),
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

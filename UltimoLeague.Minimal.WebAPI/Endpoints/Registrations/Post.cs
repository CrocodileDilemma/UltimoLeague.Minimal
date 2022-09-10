using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Registrations
{
    [HttpPost("registrations/{teamId}/{playerId}")]
    public class Post : Endpoint<RegistrationRequest, RegistrationDto>
    {
        private readonly RegistrationService _service;

        public Post(RegistrationService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(RegistrationRequest request, CancellationToken ct)
        {
            Result<Registration> result = await _service.Post(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value.Adapt<RegistrationDto>(),
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

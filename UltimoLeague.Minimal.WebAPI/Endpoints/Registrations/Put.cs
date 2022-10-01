using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Registrations
{
    [HttpPut("registrations/{id}")]
    [Authorize(Policy = Policy.AdminOnly)]
    public class Put : Endpoint<IdRequest, RegistrationDto>
    {
        private readonly RegistrationService _service;
        public Put(RegistrationService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<RegistrationDto> result = await _service.Put(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value,
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

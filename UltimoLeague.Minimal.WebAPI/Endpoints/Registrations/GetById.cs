using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Registrations
{
    [HttpGet("registrations/{id}")]
    public class GetById : Endpoint<IdRequest, RegistrationDto>
    {
        private readonly RegistrationService _service;

        public GetById(RegistrationService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<RegistrationDto> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, ct);
        }
    }
}

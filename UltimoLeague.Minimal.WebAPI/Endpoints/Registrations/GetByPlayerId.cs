using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Registrations
{
    [HttpGet("registrations/getByPlayerId/{id}")]
    public class GetByPlayerId : Endpoint<IdRequest, IEnumerable<RegistrationDto>>
    {
        private readonly RegistrationService _service;

        public GetByPlayerId(RegistrationService service)
        {
            _service = service;
        }
        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<RegistrationDto>> result = _service.GetByPlayerId(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, ct);
        }
    }
}

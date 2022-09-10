using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Registrations
{
    [HttpGet("registrations/getByTeamId/{id}")]
    public class GetByTeamId : Endpoint<IdRequest, IEnumerable<RegistrationDto>>
    {
        private readonly RegistrationService _service;

        public GetByTeamId(RegistrationService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<IEnumerable<RegistrationDto>> result = _service.GetByTeamId(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, ct);
        }
    }
}

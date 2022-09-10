using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Teams
{
    [HttpPut("teams/{id}")]
    public class Put : Endpoint<TeamUpdateRequest, TeamDto>
    {
        private readonly TeamService _service;
        public Put(TeamService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(TeamUpdateRequest request, CancellationToken ct)
        {
            Result<TeamDto> result = await _service.Update(request);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendCreatedAtAsync<GetById>(new { Id = result.Value.Id }, result.Value,
                generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}

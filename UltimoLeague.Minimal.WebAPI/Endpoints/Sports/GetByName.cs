using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sports
{
    [HttpGet("sports/getByName/{name}")]
    public class GetByName : Endpoint<NameRequest, SportDto>
    {
        private readonly IBaseService<Sport> _service;
        public GetByName(IBaseService<Sport> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(NameRequest request, CancellationToken ct)
        {
            Result<Sport> result = await _service.GetSingleByValue(x => x.SportName == request.Name);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<SportDto>(), ct);
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sports
{
    [HttpGet("sports/{id}")]
    public class GetById : Endpoint<IdRequest, SportDto>
    {
        private readonly IBaseService<Sport> _service;

        public GetById(IBaseService<Sport> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<Sport> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<SportDto>(), ct);           
        }
    }
}

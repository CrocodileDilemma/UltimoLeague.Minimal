using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sports
{
    [AllowAnonymous]
    [HttpGet("sports")]
    public class GetAll : EndpointWithoutRequest<IEnumerable<SportDto>>
    {
        private readonly IBaseService<Sport> _service;
        public GetAll(IBaseService<Sport> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = _service.GetAll().AsEnumerable()
                .Adapt<IEnumerable<SportDto>>();

            await SendOkAsync(result, ct);
        }
    }
}

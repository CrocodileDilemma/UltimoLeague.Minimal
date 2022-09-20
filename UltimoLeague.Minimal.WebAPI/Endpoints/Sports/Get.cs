using Microsoft.AspNetCore.Authorization;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Sports
{
    [AllowAnonymous]
    [HttpGet("sports")]
    public class Get : EndpointWithoutRequest<IEnumerable<SportMinimalDto>>
    {
        private readonly IBaseService<Sport> _service;
        public Get(IBaseService<Sport> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = _service.GetAll().AsEnumerable()
                .Adapt<IEnumerable<SportMinimalDto>>();

            await SendOkAsync(result, ct);
        }
    }
}

﻿using UltimoLeague.Minimal.WebAPI.Services;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Players
{
    [HttpGet("players/{id}")]
    public class GetById : Endpoint<IdRequest, PlayerDto>
    {
        private readonly PlayerService _service;
        public GetById(PlayerService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<PlayerDto> result = _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, ct);
        }
    }
}

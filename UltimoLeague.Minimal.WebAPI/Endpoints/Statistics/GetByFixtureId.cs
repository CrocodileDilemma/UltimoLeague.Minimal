﻿using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Statistics
{
    [HttpGet("statistics/getByFixtureId/{id}")]
    public class GetByFixtureId : Endpoint<IdRequest, IEnumerable<StatisticDto>>
    {
        private readonly StatisticService _service;

        public GetByFixtureId(StatisticService service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            IEnumerable<StatisticDto> result = _service.GetByFixtureId(request.Id);
            await SendOkAsync(result, ct);
        }
    }
}

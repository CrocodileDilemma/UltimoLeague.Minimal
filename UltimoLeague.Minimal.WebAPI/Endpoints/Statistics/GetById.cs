﻿using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Statistics
{
    [HttpGet("statistics/{id}")]
    public class GetById : Endpoint<IdRequest, StatisticDto>
    {
        private readonly IBaseService<Statistic> _service;

        public GetById(IBaseService<Statistic> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(IdRequest request, CancellationToken ct)
        {
            Result<Statistic> result = await _service.GetById(request.Id);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<StatisticDto>(), ct);
        }
    }
}

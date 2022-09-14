using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Fixtures
{
    [HttpGet("fixtures/getByDate/{startDate}")]
    public class GetByDate : Endpoint<StartDateRequest, IEnumerable<FixtureDto>>
    {
        private readonly IBaseService<Fixture> _service;
        
        public GetByDate(IBaseService<Fixture> service)
        {
            _service = service;
        }

        public override async Task HandleAsync(StartDateRequest request, CancellationToken ct)
        {
            DateTime startDate = request.StartDate.Date;
            DateTime endDate = startDate.AddDays(1);

            Result<IEnumerable<Fixture>> result = _service.GetByValue(x => x.FixtureDateTime < endDate && x.FixtureDateTime > startDate);

            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value.Adapt<IEnumerable<FixtureDto>>(), ct);
        }
    }
}

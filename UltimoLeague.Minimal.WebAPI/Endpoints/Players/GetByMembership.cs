using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Services;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Endpoints.Players
{
    [HttpGet("players/getByMembership/{membershipNo}")]
    public class GetByMembership : Endpoint<MembershipNoRequest, PlayerDto>
    {
        private readonly PlayerService _service;
        public GetByMembership(PlayerService service)
        {
            _service = service;
        }
        public override async Task HandleAsync(MembershipNoRequest request, CancellationToken ct)
        {
            Result<PlayerDto> result = _service.GetByMembershipNo(request.MembershipNo);
            if (result.IsFailed)
            {
                ThrowError(result.Errors[0].Message);
            }

            await SendOkAsync(result.Value, ct);
        }
    }
}

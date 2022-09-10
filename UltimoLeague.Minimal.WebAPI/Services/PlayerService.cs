using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IMongoRepository<Player> _repository;
        private readonly IMongoRepository<Team> _teamRepository;

        public PlayerService(IMongoRepository<Player> repository, IMongoRepository<Team> teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }
        public Result<PlayerDto> GetById(string id)
        {
            var result = this.PlayerQuery().FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                return Result.Fail<PlayerDto>(new ObjectNotFound<Player>().Message);
            }

            return Result.Ok(result);
        }

        public Result<PlayerDto> GetByMembershipNo(string membershipNo)
        {
            var result = this.PlayerQuery().FirstOrDefault(x => x.MembershipNumber == membershipNo);

            if (result is null)
            {
                return Result.Fail<PlayerDto>(new ObjectNotFound<Player>().Message);
            }

            return Result.Ok(result);
        }

        public IEnumerable<PlayerDto> GetByTeamId(string id)
        { 
            var result = this.PlayerQuery().Where(x => x.ActiveTeam.Id == id);
            return result.AsEnumerable();
        }

        public async Task<Result<PlayerMinimalDto>> Post(PlayerRequest request)
        {
            Player player = request.Adapt<Player>();
            player.MembershipNumber = this.GenerateMembershipNumber();

            try
            {
                await _repository.InsertOneAsync(player);
                return Result.Ok(player.Adapt<PlayerMinimalDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<PlayerMinimalDto>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Result<PlayerMinimalDto>> Update(PlayerUpdateRequest request)
        {
            var player = _repository.FindById(request.Id);
            if (player is null)
            {
                return Result.Fail<PlayerMinimalDto>(new ObjectNotFound<Player>().Message);
            }

            request.Adapt(player);

            try
            {
                await _repository.ReplaceOneAsync(player);
                return Result.Ok(player.Adapt<PlayerMinimalDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<PlayerMinimalDto>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private string GenerateMembershipNumber()
        {
            return "qqeeqeqweqe";
        }

        private IQueryable<PlayerDto> PlayerQuery()
        {
            return (from p in _repository.AsQueryable()
                    join t in _teamRepository.AsQueryable()
                    on p.ActiveTeamId equals t.Id
                    select new PlayerDto
                    {
                        Id = p.Id.ToString(),
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Active = p.Active,
                        EmailAddress = p.EmailAddress,
                        ContactNumber = p.ContactNumber,
                        DateOfBirth = p.DateOfBirth,
                        Gender = p.Gender,
                        MembershipNumber = p.MembershipNumber,
                        ActiveTeam = new TeamBaseDto
                        {
                            Id = t.Id.ToString(),
                            Code = t.Code
                        }
                    });
        }
    }
}


using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;
using UltimoLeague.Minimal.WebAPI.Utilities;

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
            var result = Queries.PlayerQuery(_repository, _teamRepository).FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                return Result.Fail<PlayerDto>(BaseErrors.ObjectNotFound<Player>());
            }

            return Result.Ok(result);
        }

        public Result<PlayerDto> GetByMembershipNo(string membershipNo)
        {
            var result = Queries.PlayerQuery(_repository, _teamRepository).FirstOrDefault(x => x.MembershipNumber == membershipNo);

            if (result is null)
            {
                return Result.Fail<PlayerDto>(BaseErrors.ObjectNotFound<Player>());
            }

            return Result.Ok(result);
        }

        public IEnumerable<PlayerDto> GetByTeamId(string id)
        { 
            var result = Queries.PlayerQuery(_repository, _teamRepository).Where(x => x.ActiveTeam.TeamId == id);
            return result.AsEnumerable();
        }

        public async Task<Result<PlayerMinimalDto>> Post(PlayerRequest request)
        {
            Player player = request.Adapt<Player>();
            player.MembershipNumber = Generators.MembershipNumber();

            try
            {
                await _repository.InsertOneAsync(player);
                return Result.Ok(player.Adapt<PlayerMinimalDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<PlayerMinimalDto>(BaseErrors.OperationFailed(ex));
            }
        }

        public async Task<Result<PlayerMinimalDto>> Update(PlayerUpdateRequest request)
        {
            var player = _repository.FindById(request.Id);
            if (player is null)
            {
                return Result.Fail<PlayerMinimalDto>(BaseErrors.ObjectNotFound<Player>());
            }

            request.Adapt(player);

            try
            {
                await _repository.ReplaceOneAsync(player);
                return Result.Ok(player.Adapt<PlayerMinimalDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<PlayerMinimalDto>(BaseErrors.OperationFailed(ex));
            }
        }
    }
}


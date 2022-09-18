using MongoDB.Bson;
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
        public async Task<Result<PlayerDto>> GetById(string id)
        {
            var result = await _repository.FindByIdAsync(id);

            if (result is null)
            {
                return Result.Fail<PlayerDto>(BaseErrors.ObjectNotFound<Player>());
            }

            return Result.Ok(result.Adapt<PlayerDto>());
        }

        public async Task<Result<PlayerDto>> GetByMembershipNo(string membershipNo)
        {
            var result = await _repository.FindOneAsync(x => x.MembershipNumber == membershipNo);

            if (result is null)
            {
                return Result.Fail<PlayerDto>(BaseErrors.ObjectNotFound<Player>());
            }

            return Result.Ok(result.Adapt<PlayerDto>());
        }

        public async Task<Result<PlayerDto>> GetByEmailAddress(string emailAddress)
        {
            var result = await _repository.FindOneAsync(x => x.EmailAddress == emailAddress);

            if (result is null)
            {
                return Result.Fail<PlayerDto>(BaseErrors.ObjectNotFound<Player>());
            }

            return Result.Ok(result.Adapt<PlayerDto>());
        }

        public IEnumerable<PlayerDto> GetByTeamId(string id)
        {
            var teamId = new ObjectId(id);
            var result = _repository.FilterBy(x => x.ActiveTeam.BaseId == teamId);
            return result.Adapt<IEnumerable<PlayerDto>>();
        }

        public async Task<Result<PlayerDto>> Post(PlayerRequest request)
        {
            var players = _repository.FilterBy(x => x.EmailAddress == request.EmailAddress);
            if (players.Any())
            {
                return Result.Fail<PlayerDto>(BaseErrors.ObjectExists<Player>());
            }

            Player player = request.Adapt<Player>();
            player.MembershipNumber = Generators.MembershipNumber();

            try
            {
                await _repository.InsertOneAsync(player);
                return Result.Ok(player.Adapt<PlayerDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<PlayerDto>(BaseErrors.OperationFailed(ex));
            }
        }

        public async Task<Result<PlayerDto>> Update(PlayerUpdateRequest request)
        {
            var player = _repository.FindById(request.Id);
            if (player is null)
            {
                return Result.Fail<PlayerDto>(BaseErrors.ObjectNotFoundWithId<Player>(request.Id));
            }

            if (player.EmailAddress != request.EmailAddress)
            {
                var players = _repository.FilterBy(x => x.EmailAddress == request.EmailAddress);
                if (players.Any())
                {
                    return Result.Fail<PlayerDto>(BaseErrors.ObjectExists<PlayerDto>());
                }
            }

            request.Adapt(player);

            try
            {
                await _repository.ReplaceOneAsync(player);
                return Result.Ok(player.Adapt<PlayerDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<PlayerDto>(BaseErrors.OperationFailed(ex));
            }
        }
    }
}


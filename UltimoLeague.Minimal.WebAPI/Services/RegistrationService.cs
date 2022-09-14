using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMongoRepository<Registration> _repository;
        private readonly IMongoRepository<Team> _teamRepository;
        private readonly IMongoRepository<Player> _playerRepository;
        public RegistrationService(IMongoRepository<Registration> repository, IMongoRepository<Team> teamRepository,
            IMongoRepository<Player> playerRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Result<RegistrationDto>> GetById(string id)
        {
            var result = await _repository.FindByIdAsync(id);

            if (result is null)
            {            
                return Result.Fail<RegistrationDto>(BaseErrors.InvalidObjectId(id));
            }

            return Result.Ok(result.Adapt<RegistrationDto>());
        }

        public Result<IEnumerable<RegistrationDto>> GetByPlayerId(string id)
        {
            var playerId = new ObjectId(id);
            var result = _repository.FilterBy(x => x.Player.BaseId == playerId).OrderByDescending(x => x.RegistrationDate);
            return Result.Ok(result.Adapt<IEnumerable<RegistrationDto>>());
        }

        public Result<IEnumerable<RegistrationDto>> GetByTeamId(string id)
        {
            var teamId = new ObjectId(id);
            var result = _repository.FilterBy(x => x.Team.BaseId == teamId || 
                (x.PreviousTeam != null && x.PreviousTeam.BaseId == teamId)).OrderByDescending(x => x.RegistrationDate);
            return Result.Ok(result.Adapt<IEnumerable<RegistrationDto>>());
        }

        public async Task<Result<RegistrationDto>> Post(RegistrationRequest request)
        {
            var player = await _playerRepository.FindByIdAsync(request.PlayerId);

            if (player is null)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.ObjectNotFoundWithId<Player>(request.PlayerId));
            }

            if (!player.Active)
            {
                return Result.Fail<RegistrationDto>(PlayerErrors.PlayerInactive());
            }

            var team = await _teamRepository.FindByIdAsync(request.TeamId);

            if (team is null)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.ObjectNotFoundWithId<Team>(request.TeamId));
            }

            if (player.ActiveTeam?.BaseId == team.Id)
            {
                return Result.Fail<RegistrationDto>(PlayerErrors.PlayerAlreadyRegistered(team.Code));
            }

            string registrationNo = Generators.RegistrationNumber();
            var registration = (registrationNo, player, team).Adapt<Registration>();

            player.ActiveTeam = team.Adapt<TeamMinimal>();

            try
            {
                await _playerRepository.ReplaceOneAsync(player);
                await _repository.InsertOneAsync(registration);
                return Result.Ok(registration.Adapt<RegistrationDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.OperationFailed(ex));
            }
        }
    }
}
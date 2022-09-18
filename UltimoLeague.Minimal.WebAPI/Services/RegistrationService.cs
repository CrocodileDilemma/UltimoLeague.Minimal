using MongoDB.Bson;
using System.Linq.Expressions;
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

            try
            {
                await _repository.InsertOneAsync(registration);
                return Result.Ok(registration.Adapt<RegistrationDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.OperationFailed(ex));
            }
        }

        public async Task<Result<RegistrationDto>> Put(IdRequest request)
        {
            var registration = await _repository.FindByIdAsync(request.Id);

            if (registration is null)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.ObjectNotFoundWithId<Registration>(request.Id));
            }

            if (registration.Approved)
            {
                return Result.Fail<RegistrationDto>(RegistrationErrors.RegistrationWithIdApproved(request.Id));
            }

            var player = await _playerRepository.FindByIdAsync(registration.Player.BaseId.ToString());

            if (player is null)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.ObjectNotFoundWithId<Player>(registration.Player.BaseId));
            }

            if (!player.Active)
            {
                return Result.Fail<RegistrationDto>(PlayerErrors.PlayerInactive());
            }

            if (player.ActiveTeam?.BaseId == registration.Team.BaseId)
            {
                return Result.Fail<RegistrationDto>(PlayerErrors.PlayerAlreadyRegistered(registration.Team.Code));
            }


            registration.Approved = true;
            player.ActiveTeam = registration.Team;

            try
            {
                await _playerRepository.ReplaceOneAsync(player);
                await _repository.ReplaceOneAsync(registration);
                return Result.Ok(registration.Adapt<RegistrationDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.OperationFailed(ex));
            }
        }
    }
}
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

        public Result<RegistrationDto> GetById(string id)
        {
            var result = this.RegistratonQuery().FirstOrDefault(x => x.Id == id);

            if (result is null)
            {            
                return Result.Fail<RegistrationDto>(BaseErrors.InvalidObjectId(id));
            }

            return Result.Ok(result);
        }

        public Result<IEnumerable<RegistrationDto>> GetByPlayerId(string id)
        {
            var result = this.RegistratonQuery().Where(x => x.Player.PlayerId == id).OrderByDescending(x => x.RegistrationDate);
            return Result.Ok(result.Adapt<IEnumerable<RegistrationDto>>());
        }

        public Result<IEnumerable<RegistrationDto>> GetByTeamId(string id)
        {
            var result = this.RegistratonQuery().Where(x => x.Team.TeamId == id || 
                (x.PreviousTeam != null && x.PreviousTeam.TeamId == id)).OrderByDescending(x => x.RegistrationDate);
            return Result.Ok(result.Adapt<IEnumerable<RegistrationDto>>());
        }

        public async Task<Result<RegistrationDto>> Post(RegistrationRequest request)
        {
            var playerQuery = Queries.PlayerQuery(_playerRepository, _teamRepository);
            var player = playerQuery.FirstOrDefault(x => x.Id == request.PlayerId);

            if (player is null)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.ObjectNotFound<Player>());
            }

            if (!player.Active)
            {
                return Result.Fail<RegistrationDto>(PlayerErrors.PlayerInactive());
            }

            var team = await _teamRepository.FindByIdAsync(request.TeamId);

            if (team is null)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.ObjectNotFound<Team>());
            }

            if (player.ActiveTeam?.TeamId == team.Id.ToString())
            {
                return Result.Fail<RegistrationDto>(PlayerErrors.PlayerAlreadyRegistered(team.Code));
            }

            string registrationNo = Generators.RegistrationNumber();
            var registration = (registrationNo, player, team).Adapt<Registration>();

            Player p = player.Adapt<Player>();
            p.ActiveTeamId = team.Id;

            try
            {
                await _playerRepository.ReplaceOneAsync(p);
                await _repository.InsertOneAsync(registration);
                return Result.Ok((registration, player, team).Adapt<RegistrationDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<RegistrationDto>(BaseErrors.OperationFailed(ex));
            }
        }

        private IQueryable<RegistrationDto> RegistratonQuery()
        {
            return (from r in _repository.AsQueryable()
                    join p in _playerRepository.AsQueryable()
                    on r.PlayerId equals p.Id
                    join t in _teamRepository.AsQueryable()
                    on r.TeamId equals t.Id
                    join t1 in _teamRepository.AsQueryable()
                    on r.PreviousTeamId equals t1.Id into pt
                    from subteam in pt.DefaultIfEmpty()
                    select new RegistrationDto
                    {
                        Id = r.Id.ToString(),
                        RegistrationNumber = r.RegistrationNumber,
                        RegistrationDate = r.RegistrationDate,
                        Player = new PlayerBaseDto
                        {
                            PlayerId = p.Id.ToString(),
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        },
                        Team = new TeamBaseDto
                        {
                            TeamId = t.Id.ToString(),
                            Code = t.Code
                        },
                        PreviousTeam = subteam == null ? null :
                        new TeamBaseDto
                        {
                            TeamId = subteam.Id == default(ObjectId) ? "<None>" : subteam.Id.ToString(),
                            Code = subteam.Code
                        },
                    });
        }
    }
}
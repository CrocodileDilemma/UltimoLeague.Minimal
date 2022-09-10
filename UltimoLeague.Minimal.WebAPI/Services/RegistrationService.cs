using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

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
                return Result.Fail<RegistrationDto>(new InvalidObjectId(id).Message);
            }

            return Result.Ok<RegistrationDto>(result);
        }

        public Result<IEnumerable<RegistrationDto>> GetByPlayerId(string id)
        {
            var result = this.RegistratonQuery().Where(x => x.Player.Id == id).OrderByDescending(x => x.RegistrationDate);
            return Result.Ok(result.Adapt<IEnumerable<RegistrationDto>>());
        }

        public Result<IEnumerable<RegistrationDto>> GetByTeamId(string id)
        {
            var result = this.RegistratonQuery().Where(x => x.Team.Id == id || 
                (x.PreviousTeam != null && x.PreviousTeam.Id == id)).OrderByDescending(x => x.RegistrationDate);
            return Result.Ok(result.Adapt<IEnumerable<RegistrationDto>>());
        }

        public async Task<Result<Registration>> Post(RegistrationRequest request)
        {
            var player = await _playerRepository.FindByIdAsync(request.PlayerId);

            if (player is null)
            {
                return Result.Fail<Registration>(new ObjectNotFound<Player>().Message);
            }

            if (!player.Active)
            {
                return Result.Fail<Registration>(new ObjectNotFound<Player>().Message);
            }

            var team = await _teamRepository.FindByIdAsync(request.TeamId);

            if (team is null)
            {
                return Result.Fail<Registration>(new ObjectNotFound<Team>().Message);
            }

            if (player.ActiveTeamId == team.Id)
            {
                return Result.Fail<Registration>(new ObjectNotFound<Player>().Message);
            }

            var registration = new Registration 
            { 
                RegistrationNumber = this.GenerateRegistrationNumber(), 
                PlayerId = player.Id, 
                TeamId = team.Id,
                PreviousTeamId = player.ActiveTeamId
            };

            player.ActiveTeamId = team.Id;

            try
            {
                await _playerRepository.ReplaceOneAsync(player);
                await _repository.InsertOneAsync(registration);
                return Result.Ok<Registration>(registration);
            }
            catch (Exception ex)
            {
                return Result.Fail<Registration>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private string GenerateRegistrationNumber()
        {
            return "rr3333333";
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
                        RegistrationDate = r.CreatedAt.Date,
                        Player = new PlayerBaseDto
                        {
                            Id = p.Id.ToString(),
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        },
                        Team = new TeamBaseDto
                        {
                            Code = t.Code
                        },
                        PreviousTeam = subteam == null ? null :
                        new TeamBaseDto
                        {
                            Code = subteam.Code
                        },
                    });
        }
    }
}
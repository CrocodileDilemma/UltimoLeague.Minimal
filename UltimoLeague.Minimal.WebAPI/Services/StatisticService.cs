using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoRepository<Statistic> _repository;
        private readonly IMongoRepository<FixtureResult> _fixtureResultRepository;
        private readonly IMongoRepository<Fixture> _fixtureRepository;
        private readonly IMongoRepository<Player> _playerRepository;

        public StatisticService(IMongoRepository<Statistic> repository, IMongoRepository<FixtureResult> fixtureResultRepository,
            IMongoRepository<Fixture> fixtureRepository, IMongoRepository<Player> playerRepository)
        {
            _repository = repository;
            _fixtureResultRepository = fixtureResultRepository;
            _fixtureRepository = fixtureRepository;
            _playerRepository = playerRepository;
        }
        public Result<StatisticDto> GetById(string id)
        {
            var result = StatisticQuery().FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                return Result.Fail<StatisticDto>(BaseErrors.ObjectNotFoundWithId<Statistic>(id));
            }

            return Result.Ok(result);
        }

        public IEnumerable<StatisticDto> GetByFixtureId(string id)
        {
            var result = StatisticQuery().Where(x => x.FixtureResult.FixtureId == id);
            return result.AsEnumerable();
        }

        public IEnumerable<StatisticDto> GetByPlayerId(string id)
        {
            var result = StatisticQuery().Where(x => x.Player.Id == id);
            return result.AsEnumerable();
        }

        public async Task<Result<StatisticDto>> Post(StatisticRequest request)
        {
            var fixtureResult = await _fixtureResultRepository.FindByIdAsync(request.FixtureResultId);
            if (fixtureResult is null)
            {
                return Result.Fail<StatisticDto>(BaseErrors.ObjectNotFoundWithId<FixtureResult>(request.FixtureResultId));
            }

            var player = await _playerRepository.FindByIdAsync(request.PlayerId);
            if (player is null)
            {
                return Result.Fail<StatisticDto>(BaseErrors.ObjectNotFoundWithId<Player>(request.PlayerId));
            }

            var statistic = (request, player).Adapt<Statistic>();
            try
            {
                await _repository.InsertOneAsync(statistic);
                return this.GetById(statistic.Id.ToString());
            }
            catch (Exception ex)
            {
                return Result.Fail<StatisticDto>(BaseErrors.OperationFailed(ex));
            }

        }

        private IQueryable<StatisticDto> StatisticQuery()
        {
            return (from s in _repository.AsQueryable()
                    join r in _fixtureResultRepository.AsQueryable()
                    on s.FixtureResultId equals r.Id
                    join f in _fixtureRepository.AsQueryable()
                    on r.FixtureId equals f.Id
                    select new StatisticDto
                    {
                        Id = s.Id.ToString(),
                        Score = s.Score,
                        StatisticDateTime = s.StatisticDateTime,
                        StatisticEvent = s.StatisticEvent,
                        Player = new PlayerMinimalDto
                        {
                            Id = s.Player.BaseId.ToString(),
                            PlayerName = s.Player.PlayerName
                        },
                        FixtureResult = new FixtureResultDto
                        {
                            FixtureId = r.FixtureId.ToString(),
                            Score = r.Score,
                            Id = r.Id.ToString(),
                            Result = r.Result,
                            Team = new TeamMinimalDto
                            {
                                Code = r.Team.Code,
                                Id = r.Team.BaseId.ToString()
                            }
                        }
                    });
        }
    }
}

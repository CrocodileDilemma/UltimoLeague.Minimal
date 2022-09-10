using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Mapping;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class SeasonService : BaseService<Season>
    {
        private readonly IMongoRepository<League> _leagueRepository;
        public SeasonService(IMongoRepository<Season> repository, IMongoRepository<League> leagueRepository) : base(repository)
        {
            _leagueRepository = leagueRepository;
        }

        public async Task<Result<Season>> Post(SeasonRequest request)
        {
            var leagueId = request.LeagueId.ToObjectId();

            var league = await _leagueRepository.FindByIdAsync(request.LeagueId);

            if (league is null)
            {
                return Result.Fail<Season>(new ObjectNotFound<League>().Message);
            }

            var seasons = Repository.FilterBy(x => x.LeagueId == league.Id && x.StartDate <= request.EndDate
                && x.EndDate >= request.StartDate);

            if (seasons.Any())
            {
                return Result.Fail<Season>(new ObjectExists<Season>().Message);
            }

            var season = new Season
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                NoOfMatches = request.NoOfMatches,
                LeagueId = league.Id
            };

            return await base.Post(season);
        }
    }
}

using MongoDB.Bson;
using System.Collections.Generic;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class SeasonService : BaseService<Season>
    {
        private readonly IMongoRepository<Team> _teamRepository;
        private readonly IMongoRepository<Arena> _arenaRepository;
        private readonly IMongoRepository<League> _leagueRepository;
        public SeasonService(IMongoRepository<Season> repository, IMongoRepository<Team> teamRepository, 
            IMongoRepository<Arena> arenaRepository, IMongoRepository<League> leagueRepository) : base(repository)
        {
            _teamRepository = teamRepository;
            _arenaRepository = arenaRepository;
            _leagueRepository = leagueRepository;
        }

        public async Task<Result<Season>> Post(SeasonRequest request)
        {
            var league = await _leagueRepository.FindByIdAsync(request.LeagueId);
            if (league is null)
            {
                return Result.Fail<Season>(BaseErrors.ObjectNotFoundWithId<League>(request.LeagueId));
            }

            var teams = _teamRepository.FilterBy(x => x.LeagueId == league.Id)
                .Select(x => new TeamBaseDto { TeamId = x.Id.ToString(), Code = x.Code })
                .ToList();

            if (!teams.Any())
            {
                return Result.Fail<Season>(TeamErrors.InvalidLeague(request.LeagueId));
            }

            var seasons = Repository.FilterBy(x => x.LeagueId == league.Id && x.StartDate <= request.EndDate
                && x.EndDate >= request.StartDate);

            if (seasons.Any())
            {
                return Result.Fail<Season>(BaseErrors.ObjectExists<Season>());
            }

            var arenas = _arenaRepository.AsQueryable();
            
            // get number of fixtures allowed per day and for each week
            List<TimeOnly> fixturesPerDay = Generators.GetFixturesPerDay(league.Sport, request.StartTime, request.EndTime);
            int fixturesPerWeek = request.MatchDays.Count * fixturesPerDay.Count * arenas.Count();

            // determine how many fixtures are required per week.
            int teamFixtures = teams.Count() / 2;
            // add a bye to the teams list if there are an uneven number of teams.
            if (teams.Count() % 2 == 1)
            {
                teams.Add(Generators.ByeFixture());
            }
            // randomise the teams
            teams.Shuffle();

            // if there are more fixtures required per week than available, the season parameters are unacceptable
            if (teamFixtures > fixturesPerWeek)
            {
                return Result.Fail<Season>(SeasonErrors.UnacceptableSeason(teamFixtures, fixturesPerWeek));
            }

            var season = (request, league.Id, ObjectId.GenerateNewId()).Adapt<Season>();
            List<Fixture> fixtures = Generators.GenerateFixtures(teams, arenas, request, fixturesPerDay, season.Id);
            
            return await base.Post(season);
        }
    }
}

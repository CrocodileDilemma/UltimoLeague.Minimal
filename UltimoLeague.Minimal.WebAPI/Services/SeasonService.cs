using MongoDB.Bson;
using System.Collections.Generic;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly IMongoRepository<Season> _repository;
        private readonly IMongoRepository<Team> _teamRepository;
        private readonly IMongoRepository<Arena> _arenaRepository;
        private readonly IMongoRepository<League> _leagueRepository;
        private readonly IMongoRepository<Sport> _sportRepository;
        private readonly IMongoRepository<Fixture> _fixtureRepository;

        public SeasonService(IMongoRepository<Season> repository, IMongoRepository<Team> teamRepository,
            IMongoRepository<Arena> arenaRepository, IMongoRepository<League> leagueRepository,
            IMongoRepository<Sport> sportRepository, IMongoRepository<Fixture> fixtureRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _arenaRepository = arenaRepository;
            _leagueRepository = leagueRepository;
            _sportRepository = sportRepository;
            _fixtureRepository = fixtureRepository;
        }

        public async Task<Result<SeasonBaseDto>> Post(SeasonRequest request)
        {
            var league = await _leagueRepository.FindByIdAsync(request.LeagueId);
            if (league is null)
            {
                return Result.Fail<SeasonBaseDto>(BaseErrors.ObjectNotFoundWithId<League>(request.LeagueId));
            }

            var sport = await _sportRepository.FindByIdAsync(league.Sport.BaseId.ToString());
            if (sport is null)
            {
                return Result.Fail<SeasonBaseDto>(BaseErrors.ObjectNotFoundWithId<Sport>(league.Sport.BaseId.ToString()));
            }

            var teams = _teamRepository.FilterBy(x => x.League.BaseId == league.Id)
                .Select(x => new TeamMinimal { BaseId = x.Id, Code = x.Code })
                .ToList();

            if (!teams.Any())
            {
                return Result.Fail<SeasonBaseDto>(TeamErrors.InvalidLeague(request.LeagueId));
            }

            var seasons = _repository.FilterBy(x => (x.EndDate >= request.StartDate && x.StartDate <= request.StartDate)
                 && x.League.BaseId == league.Id);

            if (seasons.Any())
            {
                return Result.Fail<SeasonBaseDto>(BaseErrors.ObjectExists<Season>());
            }

            var arenas = _arenaRepository.AsQueryable();

            // get number of fixtures allowed per day and for each week
            List<TimeOnly> fixturesPerDay = Generators.GetFixturesPerDay(sport, TimeOnly.Parse(request.StartTime), TimeOnly.Parse(request.EndTime));
            int fixturesPerWeek = request.MatchDays.Count() * fixturesPerDay.Count * arenas.Count();

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
                return Result.Fail<SeasonBaseDto>(SeasonErrors.UnacceptableSeason(teamFixtures, fixturesPerWeek));
            }

            var season = (request, league.Adapt<LeagueMinimal>(), ObjectId.GenerateNewId()).Adapt<Season>();
            List<Fixture> fixtures = Generators.GenerateFixtures(teams, arenas, request, fixturesPerDay, season.Id, league.Adapt<LeagueMinimal>());
            season.EndDate = fixtures.Max(x => x.FixtureDateTime).Date;


            try
            {
                await _repository.InsertOneAsync(season);
                await _fixtureRepository.InsertManyAsync(fixtures);

                return Result.Ok(season.Adapt<SeasonBaseDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<SeasonBaseDto>(BaseErrors.OperationFailed(ex));
            }
        }
    }
}

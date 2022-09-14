using Mapster;
using MongoDB.Bson;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class FixtureService : BaseService<Fixture>
    {
        private readonly IMongoRepository<Team> _teamRepository;
        private readonly IMongoRepository<Arena> _arenaRepository;

        public FixtureService(IMongoRepository<Fixture> repository, IMongoRepository<Team> teamRepository, 
            IMongoRepository<Arena> arenaRepository) : base(repository)
        {
            _teamRepository = teamRepository;
            _arenaRepository = arenaRepository;
        }

        public async Task<Result<Fixture>> Update(FixtureUpdateRequest request)
        {
            var fixture = Repository.FindById(request.Id);
            
            if (fixture is null)
            {
                return Result.Fail<Fixture>(BaseErrors.ObjectNotFoundWithId<Fixture>(request.Id));
            }
            
            if (!string.IsNullOrEmpty(request.TeamId) && request.TeamId != fixture.Team.BaseId.ToString())
            {
                if (request.TeamId == ObjectId.Empty.ToString())
                {
                    fixture.Team = Generators.ByeFixture();
                }
                else
                {
                    var team = _teamRepository.FindById(request.TeamId);
                    
                    if (team is null)
                    {
                        return Result.Fail<Fixture>(BaseErrors.ObjectNotFoundWithId<Team>(request.TeamId));
                    }

                    fixture.Team = team.Adapt<TeamMinimal>();
                }
            }

            if (!string.IsNullOrEmpty(request.TeamOppositionId) && request.TeamOppositionId != fixture.TeamOpposition.BaseId.ToString())
            {
                if (request.TeamOppositionId == ObjectId.Empty.ToString())
                {
                    fixture.TeamOpposition = Generators.ByeFixture();
                }
                else
                {
                    var team = _teamRepository.FindById(request.TeamOppositionId);

                    if (team is null)
                    {
                        return Result.Fail<Fixture>(BaseErrors.ObjectNotFoundWithId<Team>(request.TeamOppositionId));
                    }

                    fixture.TeamOpposition = team.Adapt<TeamMinimal>();
                }
            }

            if (fixture.Team.BaseId == ObjectId.Empty || fixture.TeamOpposition.BaseId == ObjectId.Empty)
            {
                fixture.Arena = null;
            }
            else if (!string.IsNullOrEmpty(request.ArenaId) && request.ArenaId != fixture.Arena?.Id.ToString())
            {
                var arena = _arenaRepository.FindById(request.ArenaId);

                if (arena is null)
                {
                    return Result.Fail<Fixture>(BaseErrors.ObjectNotFoundWithId<Arena>(request.ArenaId));
                }

                fixture.Arena = arena;    
            }

            if (request.FixtureDateTime is not null)
            {
                fixture.FixtureDateTime = request.FixtureDateTime.Value;
            }

            if (request.Status is not null)
            {
                fixture.Status = request.Status.Value;
            }

            if (fixture.Arena is not null)
            {
                var fixtures = Repository.FilterBy(x => x.Id != fixture.Id && x.FixtureDateTime == fixture.FixtureDateTime
                    && (x.Arena != null && x.Arena.Id == fixture.Arena.Id));

                if (fixtures.Any())
                {
                    return Result.Fail<Fixture>(FixtureErrors.FixtureClash(fixture.FixtureDateTime, fixture.Arena.ArenaName));
                }
            }

            if (fixture is null)
            {
                return Result.Fail<Fixture>(BaseErrors.ObjectNotFoundWithId<Fixture>(request.Id));
            }

            return await base.Update(fixture);
        }
    }
}

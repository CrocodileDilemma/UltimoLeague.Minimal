using Mapster;
using MongoDB.Bson;
using Namotion.Reflection;
using SharpCompress.Common;
using System.Numerics;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class TeamService : ITeamService
    {
        private readonly IMongoRepository<Team> _repository;
        private readonly IMongoRepository<League> _leagueRepository;
        public TeamService(IMongoRepository<Team> repository, IMongoRepository<League> leagueRepository)
        {
            _repository = repository;
            _leagueRepository = leagueRepository;
        }

        public IEnumerable<TeamDto> GetAll()
        {
            return TeamQuery().AsEnumerable();
        }

        public Result<TeamDto> GetById(string id)
        {
            var result = TeamQuery().FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                return Result.Fail<TeamDto>(BaseErrors.InvalidObjectId(id));
            }

            return Result.Ok(result);
        }

        public IEnumerable<TeamDto> GetByLeagueId(string id)
        {
            var result = TeamQuery().Where(x => x.League.Id == id);
            return result.AsEnumerable();
        }

        public async Task<Result<TeamDto>> Post(TeamRequest request)
        {
            Result<League> validationResult = await this.ValidateRequest(request.LeagueId, request.SportId);
            if (validationResult.IsFailed)
            {
                return Result.Fail<TeamDto>(validationResult.Errors);
            }

            var team = await _repository.FindOneAsync(x => x.Code == request.Code && x.SportId == validationResult.Value.Sport.Id);

            if (team is not null)
            {
                return Result.Fail<TeamDto>(BaseErrors.ObjectExists<Team>());
            }


            team = (request, validationResult.Value).Adapt<Team>();

            try
            {
                await _repository.InsertOneAsync(team);
                return Result.Ok((team, validationResult.Value).Adapt<TeamDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<TeamDto>(BaseErrors.OperationFailed(ex));
            }
        }

        public async Task<Result<TeamDto>> Update(TeamUpdateRequest request)
        {
            var team = _repository.FindById(request.Id);
            if (team is null)
            {
                return Result.Fail<TeamDto>(BaseErrors.ObjectNotFound<Team>());
            }

            Result<League> validationResult = await this.ValidateRequest(request.LeagueId ?? team.LeagueId.ToString(), 
                request.SportId ?? team.SportId.ToString());
            if (validationResult.IsFailed)
            {
                return Result.Fail<TeamDto>(validationResult.Errors);
            }

            (request, validationResult.Value).Adapt(team);

            try
            {
                await _repository.ReplaceOneAsync(team);
                return Result.Ok((team, validationResult.Value).Adapt<TeamDto>());
            }
            catch (Exception ex)
            {
                return Result.Fail<TeamDto>(BaseErrors.OperationFailed(ex));
            }
        }

        private IQueryable<TeamDto> TeamQuery()
        {
            return (from t in _repository.AsQueryable()
                    join l in _leagueRepository.AsQueryable()
                    on t.LeagueId equals l.Id
                    select new TeamDto
                    {
                        Id = t.Id.ToString(),
                        Code = t.Code,
                        ContactEmail = t.ContactEmail,
                        ContactFirstName = t.ContactFirstName,
                        ContactLastName = t.ContactLastName,
                        ContactNumber = t.ContactNumber,
                        Name = t.Name,
                        League = new LeagueBaseDto
                        {
                            Id = l.Id.ToString(),
                            Code = l.Code
                        }
                    });
        }

        private async Task<Result<League>> ValidateRequest(string leagueId, string sportId)
        {
            var league = await _leagueRepository.FindByIdAsync(leagueId);

            if (league is null)
            {
                return Result.Fail<League>(BaseErrors.ObjectNotFoundWithId<League>(leagueId));
            }

            if (league.Sport.Id != sportId.ToObjectId())
            {
                return Result.Fail<League>(LeagueErrors.InvalidSport(sportId));
            }

            return Result.Ok(league);
        }
    }
}

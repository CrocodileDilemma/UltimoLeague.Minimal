using MongoDB.Bson;
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
            return _repository.AsQueryable().AsEnumerable()
                .Adapt<IEnumerable<TeamDto>>();
        }

        public async Task<Result<TeamDto>> GetById(string id)
        {
            var result = await _repository.FindByIdAsync(id);

            if (result is null)
            {
                return Result.Fail<TeamDto>(BaseErrors.InvalidObjectId(id));
            }

            return Result.Ok(result.Adapt<TeamDto>());
        }

        public IEnumerable<TeamDto> GetByLeagueId(string id)
        {
            var result = _repository.FilterBy(x => x.League.BaseId == new ObjectId(id));
            return result.AsEnumerable().Adapt<IEnumerable<TeamDto>>();
        }

        public async Task<Result<TeamDto>> Post(TeamRequest request)
        {
            Result<League> validationResult = await this.ValidateRequest(request.LeagueId, request.SportId);
            if (validationResult.IsFailed)
            {
                return Result.Fail<TeamDto>(validationResult.Errors);
            }

            var team = await _repository.FindOneAsync(x => x.Code == request.Code && 
                x.Sport.BaseId == validationResult.Value.Sport.BaseId);

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

            Result<League> validationResult = await this.ValidateRequest(request.LeagueId ?? team.League.BaseId.ToString(), 
                request.SportId ?? team.Sport.BaseId.ToString());
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

        private async Task<Result<League>> ValidateRequest(string leagueId, string sportId)
        {
            var league = await _leagueRepository.FindByIdAsync(leagueId);

            if (league is null)
            {
                return Result.Fail<League>(BaseErrors.ObjectNotFoundWithId<League>(leagueId));
            }

            if (league.Sport.BaseId != sportId.ToObjectId())
            {
                return Result.Fail<League>(LeagueErrors.InvalidSport(sportId));
            }

            return Result.Ok(league);
        }
    }
}

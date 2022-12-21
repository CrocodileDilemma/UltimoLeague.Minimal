using Org.BouncyCastle.Asn1.Ocsp;
using SharpCompress.Common;
using UltimoLeague.Minimal.DAL.Common;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class LeagueService : BaseService<League>
    {
        private readonly IMongoRepository<Sport> _sportRepository;
        public LeagueService(IMongoRepository<League> repository, IMongoRepository<Sport> sportRepository) : base(repository)
        {
            _sportRepository = sportRepository;
        }

        public async Task<Result<League>> Post(LeagueRequest request)
        {
            var validationResult = await this.ValidateLeague(request.SportId, request.Code, request.Gender, request.Level);

            if (validationResult.IsFailed)
            {
                return Result.Fail<League>(validationResult.Errors);
            }

            var league = (request, validationResult.Value).Adapt<League>();

            return await base.Post(league);
        }

        public async Task<Result<League>> Update(LeagueUpdateRequest request)
        {
            var validationResult = await this.ValidateLeague(request.SportId, request.Code, request.Gender, request.Level, request.Id);

            if (validationResult.IsFailed)
            {
                return Result.Fail<League>(validationResult.Errors);
            }

            var league = (request, validationResult.Value).Adapt<League>();

            return await base.Update(league);
        }

        private async Task<Result<Sport>> ValidateLeague(string sportId, string code, Gender gender, int level, string id = "")
        {
            var sport = await _sportRepository.FindByIdAsync(sportId);

            if (sport is null)
            {
                return Result.Fail(BaseErrors.ObjectNotFoundWithId<Sport>(sportId));
            }

            var league = await Repository.FindOneAsync(x => x.Code == code &&
                x.Sport.BaseId == sport.Id && x.Gender == gender && x.Id.ToString() != id);

            if (league is not null)
            {
                return Result.Fail(BaseErrors.ObjectExists<League>());
            }

            league = await Repository.FindOneAsync(x => x.Level == level &&
                x.Sport.BaseId == sport.Id && x.Gender == gender && x.Id.ToString() != id);

            if (league is not null)
            {
                return Result.Fail(BaseErrors.ObjectExists<League>());
            }

            return Result.Ok(sport);
        }
    }
}

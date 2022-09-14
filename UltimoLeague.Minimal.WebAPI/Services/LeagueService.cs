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
            var sport = await _sportRepository.FindByIdAsync(request.SportId);

            if (sport is null)
            {
                return Result.Fail<League>(BaseErrors.ObjectNotFoundWithId<Sport>(request.SportId));
            }

            var league = await Repository.FindOneAsync(x => x.Code == request.Code &&
                x.Sport.BaseId == sport.Id && x.Gender == request.Gender);

            if (league is not null)
            {
                return Result.Fail<League>(BaseErrors.ObjectExists<League>());
            }

            league = await Repository.FindOneAsync(x => x.Level == request.Level &&
                x.Sport.BaseId == sport.Id && x.Gender == request.Gender);

            if (league is not null)
            {
                return Result.Fail<League>(BaseErrors.ObjectExists<League>());
            }

            league = (request, sport).Adapt<League>();

            return await base.Post(league);
        }
    }
}

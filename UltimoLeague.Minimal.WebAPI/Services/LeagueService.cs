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
                return Result.Fail<League>(new ObjectNotFound<Sport>().Message);
            }

            var league = await Repository.FindOneAsync(x => x.Code == request.Code &&
                x.Sport.Id == sport.Id && x.Gender == request.Gender);

            if (league is not null)
            {
                return Result.Fail<League>(new ObjectExists<League>().Message);
            }

            league = await Repository.FindOneAsync(x => x.Level == request.Level &&
                x.Sport.Id == sport.Id && x.Gender == request.Gender);

            if (league is not null)
            {
                return Result.Fail<League>(new ObjectExists<League>().Message);
            }

            league = new League
            {
                Sport = sport,
                Code = request.Code,
                Level = request.Level,
                Gender = request.Gender,
                Name = request.Name
            };

            return await base.Post(league);
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class SportService : BaseService<Sport>
    {
        public SportService(IMongoRepository<Sport> repository) : base(repository){}

        public async Task<Result<Sport>> Post(string sportName)
        {
            var sport = await Repository.FindOneAsync(x => x.SportName == sportName);

            if (sport is not null)
            {
                return Result.Fail<Sport>(new ObjectExists<Sport>().Message);
            }

            sport = new Sport { SportName = sportName };

            return await base.Post(sport);          
        }
    }
}

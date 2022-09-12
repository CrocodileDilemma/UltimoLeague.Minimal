using System.Numerics;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class SportService : BaseService<Sport>
    {
        public SportService(IMongoRepository<Sport> repository) : base(repository){}

        public async Task<Result<Sport>> Post(SportRequest request)
        {
            var sport = await Repository.FindOneAsync(x => x.SportName == request.SportName);

            if (sport is not null)
            {
                return Result.Fail<Sport>(BaseErrors.ObjectExists<Sport>());
            }

            sport = request.Adapt<Sport>();

            return await base.Post(sport);          
        }

        public async Task<Result<Sport>> Update(SportUpdateRequest request)
        {
            var sport = await Repository.FindByIdAsync(request.Id);

            if (sport is null)
            {
                return Result.Fail<Sport>(BaseErrors.ObjectNotFoundWithId<Sport>(request.Id));
            }

            request.Adapt(sport);

            return await base.Update(sport);
        }
    }
}

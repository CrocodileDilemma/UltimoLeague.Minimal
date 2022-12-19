using System.Numerics;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Utilities;

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

        internal void GenerateSport()
        {
            var sport = Repository.FindOne(x => !string.IsNullOrEmpty(x.SportName));
            if (sport is null)
            {
                sport = new Sport
                {
                    SportName = "Football",
                    Duration = 90,
                    Leeway = 15,
                    PointsForDraw = 1,
                    PointsForWin = 3,
                    PointsForForfeit = 3                                      
                };

                base.Post(sport);
            }
        }
    }
}

using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class ArenaService : BaseService<Arena>
    {
        public ArenaService(IMongoRepository<Arena> repository) : base(repository) { }

        public async Task<Result<Arena>> Post(ArenaRequest request)
        {
            var arena = await Repository.FindOneAsync(x => x.ArenaName == request.ArenaName);

            if (arena is not null)
            {
                return Result.Fail<Arena>(BaseErrors.ObjectExists<Arena>());
            }

            arena = request.Adapt<Arena>();

            return await base.Post(arena);
        }
    }
}

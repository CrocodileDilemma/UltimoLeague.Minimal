using FluentResults;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class ArenaService : BaseService<Arena>
    {
        public ArenaService(IMongoRepository<Arena> repository) : base(repository) { }

        public async Task<Result<Arena>> Post(string arenaName)
        {
            var arena = await Repository.FindOneAsync(x => x.ArenaName == arenaName);

            if (arena is not null)
            {
                return Result.Fail<Arena>(BaseErrors.ObjectExists<Arena>());
            }

            arena = new Arena { ArenaName = arenaName };

            return await base.Post(arena);
        }
    }
}

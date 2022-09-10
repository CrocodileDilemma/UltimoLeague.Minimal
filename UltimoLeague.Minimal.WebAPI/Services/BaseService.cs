using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class BaseService<T> : IBaseService<T> where T : IBaseEntity
    {
        private readonly IMongoRepository<T> _repository;
        public IMongoRepository<T> Repository
        {
            get { return this._repository; }
        }

        public BaseService(IMongoRepository<T> repository)
        {
            this._repository = repository;
        }

        public async Task<Result<T>> GetById(string id)
        {                    
            var entity = await _repository.FindByIdAsync(id);
            if (entity is null)
            {
                return Result.Fail<T>(new ObjectNotFoundWithId<T>(id).Message);
            }

            return Result.Ok<T>(entity);
        }

        public IMongoQueryable<T> QueryAll()
        {
            var query = _repository.AsQueryable();
            return (IMongoQueryable<T>)query;
        }

        public IQueryable<T> GetAll()
        {
            return _repository.AsQueryable();
        }

        public async Task<Result<T>> GetSingleByValue(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.FindOneAsync(expression);
            if (entity is null)
            {
                return Result.Fail<T>(new ObjectNotFound<T>().Message);
            }

            return Result.Ok<T>(entity);
        }

        public Result<IEnumerable<T>> GetByValue(Expression<Func<T, bool>> expression)
        {
            var entities = _repository.FilterBy(expression);
            if (entities is null || !entities.Any())
            {
                return Result.Fail<IEnumerable<T>>(new ObjectsNotFound<T>().Message);
            }

            return Result.Ok<IEnumerable<T>>(entities);
        }

        public async Task<Result<T>> Post(T entity)
        {
            try
            {
                await _repository.InsertOneAsync(entity);
                return Result.Ok<T>(entity);
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Result<T>> Update(T entity)
        {
            try
            {
                await _repository.ReplaceOneAsync(entity);
                return Result.Ok<T>(entity);
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}

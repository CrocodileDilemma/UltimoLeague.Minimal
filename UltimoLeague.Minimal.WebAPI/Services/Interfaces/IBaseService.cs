using FluentResults;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Services.Interfaces
{
    public interface IBaseService<T> where T : IBaseEntity
    {
        Task<Result<T>> GetById(string id);
        IMongoQueryable<T> QueryAll();
        IQueryable<T> GetAll();
        Task<Result<T>> GetSingleByValue(Expression<Func<T, bool>> expression);
        Result<IEnumerable<T>> GetByValue(Expression<Func<T, bool>> expression);
        Task<Result<T>> Post(T entity);
        Task<Result<T>> Update(T entity);
    }
}

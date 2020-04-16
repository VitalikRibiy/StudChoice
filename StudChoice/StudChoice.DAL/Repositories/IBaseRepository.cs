using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudChoice.DAL.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(long id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Remove(params object[] keys);

        TEntity Remove(TEntity entity);

        TEntity Update(TEntity entity);
    }
}

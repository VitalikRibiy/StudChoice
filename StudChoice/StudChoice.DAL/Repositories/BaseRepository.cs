using Microsoft.EntityFrameworkCore;
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudChoice.DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class ,IBaseModel
    {
        private readonly EFDBContext _context;
        protected DbSet<TEntity> _entities;

        public BaseRepository(EFDBContext dbContext)
        {
            _context = dbContext;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await Entities.AddAsync(entity)).Entity;
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TEntity>>(Entities);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<TEntity>>(Entities.Where(predicate));
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return Entities.SingleOrDefaultAsync(t => t.Id.Equals(id));
        }

        public virtual Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount)
        {
            return Task.FromResult<IEnumerable<TEntity>>(Entities.Skip((int)index).Take((int)amount));
        }

        public virtual TEntity Remove(params object[] keys)
        {
            var model = Entities.Find(keys);
            if (model != null)
            {
                model = Entities.Remove(model).Entity;
            }

            return model;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return Entities.Remove(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return Entities.Update(entity).Entity;
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                return _entities ?? (_entities = _context.Set<TEntity>());
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Repositories
{
    /// <summary>
    /// Interface repository base
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="K">Type of id column</typeparam>
    /// <typeparam name="TContext">DbContext</typeparam>
    public class RepositoryBaseAsync<T, K, TContext> : IRepositoryBaseAsync<T, K, TContext> where T : EntityBase<K> where TContext : DbContext
    {
        private readonly TContext dbContext;
        private readonly IUnitOfWork<TContext> unitOfWork;
        public RepositoryBaseAsync(TContext _dbContext, IUnitOfWork<TContext> _unitOfWork)
        {
            dbContext = _dbContext;
            unitOfWork = _unitOfWork;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await dbContext.Database.BeginTransactionAsync();
        }

        public async Task<K> CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity.Id;
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            return entities.Select(x => x.Id).ToList();
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await dbContext.Database.CommitTransactionAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
        {
            return trackChanges ? dbContext.Set<T>().AsNoTracking().Where(expression)
                : dbContext.Set<T>().Where(expression);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false)
        {
            return trackChanges ? await dbContext.Set<T>().AsNoTracking().Where(expression).ToListAsync()
                : await dbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return await items.ToListAsync();
        }

        public async Task<int> CountByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return await items.CountAsync();
        }

        public IQueryable<T> GetAll(bool trackChanges = false)
        {
            return trackChanges ? dbContext.Set<T>().AsNoTracking()
                : dbContext.Set<T>();
        }

        public IQueryable<T> GetAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = GetAll(trackChanges);
            return items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<List<T>> GetAllAsync(bool trackChanges = false)
        {
            return trackChanges ? await dbContext.Set<T>().AsNoTracking().ToListAsync()
                : await dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = GetAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return await items.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(K id)
        {
            return await FindByCondition(x => x.Id.Equals(id) && x.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindByCondition(x => x.Id.Equals(id) && x.Active, false, includeProperties)
                .FirstOrDefaultAsync();
        }

        public Task HardDeleteAsync(K id)
        {
            var obj = FindByCondition(x => x.Id.Equals(id) && x.Active).FirstOrDefault();
            if (obj != null)
            {
                dbContext.Set<T>().Remove(obj);
            }
            return Task.CompletedTask;
        }

        public Task HardDeleteListAsync(IEnumerable<K> ids)
        {
            var objs = FindByCondition(x => ids.Contains(x.Id) && x.Active);
            if (objs != null && objs.Count() > 0)
            {
                dbContext.Set<T>().RemoveRange(objs);
            }
            return Task.CompletedTask;
        }

        public async Task RollbackTransactionAsync()
        {
            await dbContext.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public async Task<bool> SoftDeleteAsync(K id)
        {
            var obj = await GetByIdAsync(id);
            if (obj != null)
            {
                obj.Active = false;

                dbContext.Attach(obj);
                dbContext.Entry(obj).Property(x => x.Active).IsModified = true;
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteListAsync(IEnumerable<K> ids)
        {
            var objs = await FindByConditionAsync(x => ids.Contains(x.Id) && x.Active);
            if (objs != null && objs.Count > 0)
            {
                foreach (var obj in objs)
                {
                    obj.Active = false;

                    dbContext.Attach(obj);
                    dbContext.Entry(obj).Property(x => x.Active).IsModified = true;
                }
                return true;
            }
            return false;
        }

        public Task UpdateAsync(T entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;
            T exist = dbContext.Set<T>().Find(entity.Id);
            dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task UpdateListAsync(IEnumerable<T> entities)
        {
            dbContext.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}

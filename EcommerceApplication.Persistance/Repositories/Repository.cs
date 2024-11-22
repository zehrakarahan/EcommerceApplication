using EcommerceApplication.Persistance.Context;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EcommerceContext _dbContext;

        public Repository(EcommerceContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public virtual IQueryable<T> Entity => _dbContext.Set<T>();

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<int> CountTotalAsync()
        {
            return await EntityFrameworkQueryableExtensions.CountAsync(_dbContext.Set<T>());
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_dbContext
                    .Set<T>());
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            return  entity;
        }

        public virtual async Task UpdateAsyncEmpty(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
             await Task.CompletedTask;
        }
        public virtual async Task<bool> UpdateAsyncBool(int Id, T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }

            var existingEntity = await _dbContext.Set<T>().FindAsync(Id);
            if (existingEntity == null)
            {
               
                return false;
            }
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            var saveResult = await _dbContext.SaveChangesAsync();

            return saveResult > 0;
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
             
                return false;
            }
            _dbContext.Set<T>().Remove(entity);
            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult > 0;
        }

        public async Task<IReadOnlyList<T>> GetListAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
    }
}

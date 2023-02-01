﻿using HP.Core.Common;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HP.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected HpReadDbContext _dbContext;
        public BaseRepository(HpReadDbContext hpdbContext)
        {
            _dbContext = hpdbContext;
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.AddRangeAsync();
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e=> e.Id == id);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).Any();
        }
        public virtual async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync();
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        public virtual async Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _dbContext.Set<T>().Where(filterExpression).FirstOrDefaultAsync();
        }
    }
}

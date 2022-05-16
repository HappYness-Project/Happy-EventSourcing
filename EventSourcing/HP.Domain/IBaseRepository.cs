﻿using HP.Domain.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HP.Domain
{
    public interface IBaseRepository<T> : IDisposable where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        IFindFluent<T, T> Find(FilterDefinition<T> filter);
        public IFindFluent<T, T> Find(Expression<Func<T, bool>> filter);
        Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T replacement);
        public bool Exists(Expression<Func<T, bool>> predicate);
        Task<long> CountAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetSingleBySpecAsync(Expression<Func<T, bool>> spec);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAllAsyncInit();
        Task<List<T>> ListBySpecAsync(Expression<Func<T, bool>> spec);
        Task<List<T>> ListBySpecAsyncInit(Expression<Func<T, bool>> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
      
    }
}

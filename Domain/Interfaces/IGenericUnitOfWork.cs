using System;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IGenericUnitOfWork
    {
        IAsyncRepository<T> AsyncRepository<T>() where T : class;
        IRepository<T> Repository<T>() where T : class;
        bool Commit();
        Task<bool> CommitAsync();
        //Task<bool> LogException(string actionName, Exception ex);
        void DetachEntity<T>(T entity) where T : class;

    }
}

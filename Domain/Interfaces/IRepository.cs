using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        T GetSingleBySpec(Expression<Func<T, bool>> spec);
        IEnumerable<T> GetNextTransactionNumber();
        IEnumerable<T> ListAll();
        IQueryable<T> ListAllData();
        IEnumerable<T> ListBySpec(Expression<Func<T, bool>> spec);
        IQueryable<T> ListBySpecData(Expression<Func<T, bool>> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteList(List<T> entity);

        IEnumerable<T> GetInventoryDetail(string lpnNumber);
        int WriteTOEDIInbound();
        IEnumerable<T> ExecQuery(string query, params object[] parameters);
      
    }
}

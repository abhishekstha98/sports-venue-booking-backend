using Domain.Interface;
using Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class EFRepository<T> : IRepository<T>, IAsyncRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _agileDBContext;

        public EFRepository(ApplicationDbContext agileDBContext)
        {
            agileDBContext.Database.SetCommandTimeout(0);
            _agileDBContext = agileDBContext;
        }

        public T GetSingleBySpec(Expression<Func<T, bool>> spec)
        {
            return ListBySpec(spec).SingleOrDefault();
        }

        public async Task<T> GetSingleBySpecAsync(Expression<Func<T, bool>> spec)
        {
            return await _agileDBContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(spec);
        }

        public IEnumerable<T> ListAll()
        {
            return _agileDBContext.Set<T>().AsNoTracking().AsEnumerable();
        }

        public IQueryable<T> ListAllData()
        {
            return  _agileDBContext.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _agileDBContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> ListAllAsyncInit()
        {
            return await _agileDBContext.Set<T>().AsNoTracking().Take(10).ToListAsync();
        }

        public IEnumerable<T> ListBySpec(Expression<Func<T, bool>> spec)
        {
            IEnumerable<T> query =_agileDBContext.Set<T>().Where(spec).AsNoTracking().AsEnumerable();
            return query;
        }
        public IQueryable<T> ListBySpecData(Expression<Func<T, bool>> spec)
        {
            IQueryable<T> query = _agileDBContext.Set<T>().Where(spec).AsNoTracking().AsQueryable();
            return query;
        }
        public async Task<List<T>> ListBySpecAsyncInit(Expression<Func<T, bool>> spec)
        {
            var query = await _agileDBContext.Set<T>().AsNoTracking().Where(spec).Take(10).ToListAsync();
            return query;
        }
        public async Task<List<T>> ListBySpecAsync(Expression<Func<T, bool>> spec)
        {
            var query = await _agileDBContext.Set<T>().Where(spec).AsNoTracking().ToListAsync();
            return query;
        }

        public T Add(T entity)
        {
           _agileDBContext.Set<T>().Add(entity);

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _agileDBContext.Set<T>().AddAsync(entity);

            return entity;
        }

        public void Update(T entity)
        {
           _agileDBContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(T entity)
        {
           _agileDBContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
           _agileDBContext.Set<T>().Remove(entity);
        }

        public void DeleteList(List<T> entity)
        {
           _agileDBContext.Set<T>().RemoveRange(entity);
        }

        public async Task DeleteAsync(T entity)
        {
           _agileDBContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetNextTransactionNumber()
        {
            //var sequence = await _agileDBContext.Set<T>().FirstOrDefaultAsync();
            //if (sequence == null)
            //{
            //    sequence = new TransactionSequence();
            //    _agileDBContext.TransactionSequence.Add(sequence);
            //}
            //var temp = _agileDBContext.Set<T>().FromSqlRaw("SELECT NEXT VALUE FOR newTransactionNumber;"); 
            _agileDBContext.Database.ExecuteSqlRaw("exec SP_Next_Seq;"); 
            var temp = _agileDBContext.Set<T>().AsEnumerable();
            //_agileDBContext.Database.ExecuteSqlRaw("update seq_info set transactionnumber_seq = null;"); 
            //await _unitOfWork.SaveChangesAsync();
            return temp;
        }
        public IEnumerable<T> GetInventoryDetail(string lpnNumber)
        {
            try
            {
                var parameter = new SqlParameter("lpnNumber", lpnNumber);
                var data=  _agileDBContext.Set<T>().FromSqlRaw($"select * from dbo.Vw_Inventory_Detail WHERE LPNNumber =@lpnNumber", parameter);
                return data;
            }
            catch (Exception ex)
            {
                throw null;
            }
        }

        //public IEnumerable<T> GetClosedShipments(string customername)
        //{
        //    try
        //    {
        //        var parameter = new SqlParameter("customer", customername);
        //        var data = _agileDBContext.Set<T>().FromSqlRaw($"SELECT TOP (100) PERCENT transactionSite, customerTransaction, customerReference, AgileTransaction, consignee, consignor, postedDate, ISNULL(SUM(#LPs), 0) AS [#LPs], ISNULL(SUM(#Cases), 0) AS [#Cases], ISNULL(SUM(#Gross), 0) AS [#Gross], customerID FROM (SELECT th.transactionSite, th.customerTransaction, th.customerReference, th.transactionNumber AS AgileTransaction, tc.consignee, tc.consignor, th.postedDate, COUNT(DISTINCT l.LPNNumber) AS #LPs, SUM(ABS(u.qty))  AS #Cases, SUM(ABS(u.grossWeight)) AS #Gross, th.customerID FROM dbo.TransactionHeader AS th  INNER JOIN dbo.TransactionHeaderConsignment AS tc  ON th.transactionNumber = tc.transactionNumber INNER JOIN dbo.LPN AS l  ON th.transactionNumber = l.transactionNumber INNER JOIN  dbo.Pallet AS p  ON th.transactionNumber = p.transactionNumber AND l.LPNNumber = p.lpn INNER JOIN dbo.Units AS u ON th.transactionNumber = u.transactionNumber AND p.palletID = u.palletID WHERE (th.transactionType = 'SHIPMENT') and th.customerID = @customer GROUP BY th.transactionSite, tc.loadDesignation, th.transactionNumber, th.customerTransaction, th.customerReference, tc.consignee, tc.consignor, th.postedDate, th.customerID UNION ALL SELECT  th.transactionSite, th.customerTransaction, th.customerReference, th.transactionNumber AS AgileTransaction, tc.consignee, tc.consignor, th.postedDate, COUNT(DISTINCT l.LPNNumber) AS #LPs, SUM(ABS(u.qty)) AS #Cases, SUM(ABS(u.grossWeight)) AS #Gross, th.customerID FROM WebPortalArchive.dbo.TransactionHeader AS th INNER JOIN WebPortalArchive.dbo.TransactionHeaderConsignment AS tc ON th.transactionNumber = tc.transactionNumber INNER JOIN WebPortalArchive.dbo.LPN AS l ON th.transactionNumber = l.transactionNumber INNER JOIN WebPortalArchive.dbo.Pallet AS p  ON th.transactionNumber = p.transactionNumber AND l.LPNNumber = p.lpn INNER JOIN WebPortalArchive.dbo.Units AS u  ON th.transactionNumber = u.transactionNumber AND p.palletID = u.palletID WHERE        (th.transactionType = 'SHIPMENT') and th.customerID = @customer GROUP BY th.transactionSite, tc.loadDesignation, th.transactionNumber, th.customerTransaction, th.customerReference, tc.consignee, tc.consignor, th.postedDate, th.customerID) AS a GROUP BY transactionSite, AgileTransaction, customerTransaction, customerReference, consignee, consignor, postedDate, customerID ORDER BY transactionSite, customerTransaction", parameter);
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw null;
        //    }
        //}
        public int WriteTOEDIInbound()
        {
           int affectedRowCount= _agileDBContext.Database.ExecuteSqlRaw("exec WriteToEDI947Inbound;"); 
            //var temp = _agileDBContext.Set<T>().AsEnumerable();
            return affectedRowCount;
        }

        public IEnumerable<T> ExecQuery(string query, params object[] parameters)
        {
            try
            {
            return _agileDBContext.Set<T>().FromSqlRaw(query, parameters);
            }
            catch (Exception ex)
            {

                throw null;
            }
        }
        #region MASTER DB

      
        //public T AddEDI(T entity)
        //{
        //    _ediDBContext.Set<T>().Add(entity);

        //    return entity;
        //}

        //public async Task<T> AddEDIAsync(T entity)
        //{
        //    await _ediDBContext.Set<T>().AddAsync(entity);

        //    return entity;
        //}

        #endregion
    }
}


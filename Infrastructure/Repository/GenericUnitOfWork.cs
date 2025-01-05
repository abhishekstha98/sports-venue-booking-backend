
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interface;
using Infrastructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure.Repository
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private ApplicationDbContext _agileDBContext;

        public GenericUnitOfWork(ApplicationDbContext agileDBContext)
        {
            _agileDBContext = agileDBContext;
        }

        public Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        public IRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)) == true)
            {
                return Repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new EFRepository<T>(_agileDBContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }
        public bool Commit()
        {
            try
            {
                _agileDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                WriteTextLog("Commit ", ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
               // Log.Error(ex, ex.Message);
                return false;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }
        public async Task<bool> CommitAsync()
        {
            try
            {
                await _agileDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                WriteTextLog("Commit Async", ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
               // Log.Error(ex, ex.Message);
                return false;
            }
            finally
            {
                GC.SuppressFinalize(this);

            }
        }
        public IAsyncRepository<T> AsyncRepository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)) == true)
            {
                return Repositories[typeof(T)] as IAsyncRepository<T>;
            }
            IAsyncRepository<T> repo = new EFRepository<T>(_agileDBContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }
        //public async Task<bool> LogException(string actionName, Exception ex)
        //{
        //    ExceptionLog log = new ExceptionLog
        //    {
        //        DateTime = DateTime.Now,
        //        Message = ex.Message,
        //        InnerMessage = ex.InnerException == null ? string.Empty : ex.InnerException.Message,
        //        StackTrace = ex.StackTrace == null ? string.Empty : ex.StackTrace.ToString(),
        //        Source = ex.Source,
        //        ActionName = actionName
        //    };
        //    try
        //    {

        //        await AsyncRepository<ExceptionLog>().AddAsync(log);
        //        var success = Commit();
        //        if (!success)
        //        {
        //            WriteTextLog(actionName, ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
        //        }
        //    }
        //    catch (Exception _ex)
        //    {
        //        WriteTextLog(actionName, _ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
        //    }
        //    return true;
        //}
        void WriteTextLog(string actionName, string errorMessage)
        {
            string message = string.Concat(actionName, " - ", errorMessage);
            const string logPath = @"c:\AgileWMSLog";
            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                string path = string.Concat(logPath.ToString(), @"\", DateTime.Now.Year.ToString(), @"-", DateTime.Now.Month.ToString().PadLeft(2, '0'), @"-", DateTime.Now.Day.ToString().PadLeft(2, '0'), @".log"); 

                using StreamWriter writer = File.AppendText(path);
                writer.WriteLine(string.Concat(DateTime.Now.TimeOfDay, " - ", message));
            }
            catch (Exception ex)
            {
                //Log.Error(ex, ex.Message);
                //do nothing
            }
        }
        public void DetachEntity<T>(T entity) where T : class
        {
            _agileDBContext.Entry(entity).State = EntityState.Detached;
        }

        //#region Gainesville
        //public IRepository<T> RepositoryGainsville<T>() where T : class
        //{
        //    if (Repositories.Keys.Contains(typeof(T)) == true)
        //    {
        //        return Repositories[typeof(T)] as IRepository<T>;
        //    }
        //    IRepository<T> repo = new EFRepository<T>(_agileDBContext1);//, _ediDBContext);
        //    Repositories.Add(typeof(T), repo);
        //    return repo;
        //}
        //public bool CommitGainsville()
        //{
        //    try
        //    {
        //        _agileDBContext1.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteTextLog("Commit ", ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
        //        // Log.Error(ex, ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        GC.SuppressFinalize(this);
        //    }
        //}
        //public async Task<bool> CommitAsyncGainsville()
        //{
        //    try
        //    {
        //        await _agileDBContext1.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteTextLog("Commit Async", ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
        //        // Log.Error(ex, ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        GC.SuppressFinalize(this);

        //    }
        //}
        //public IAsyncRepository<T> AsyncRepositoryGainsville<T>() where T : class
        //{
        //    if (Repositories.Keys.Contains(typeof(T)) == true)
        //    {
        //        return Repositories[typeof(T)] as IAsyncRepository<T>;
        //    }
        //    IAsyncRepository<T> repo = new EFRepository<T>(_agileDBContext1);//, _ediDBContext);
        //    Repositories.Add(typeof(T), repo);
        //    return repo;
        //}
        //public void DetachEntityGainsville<T>(T entity) where T : class
        //{
        //    _agileDBContext1.Entry(entity).State = EntityState.Detached;
        //}
        //#endregion
        #region EDICommit
        //public bool EDICommit()
        //{
        //    try
        //    {
        //        _ediDBContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteTextLog("Commit ", ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
        //        // Log.Error(ex, ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        GC.SuppressFinalize(this);
        //    }
        //}
        //public async Task<bool> EDICommitAsync()
        //{
        //    try
        //    {
        //        await _ediDBContext.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteTextLog("Commit Async", ex.Message.ToString() + "  :::::  " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
        //        // Log.Error(ex, ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        GC.SuppressFinalize(this);

        //    }
        //}

        #endregion

   
    }
}


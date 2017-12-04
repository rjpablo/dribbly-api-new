using DribblyAPI.Entities;
using DribblyAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Security.Claims;
using NLog;

namespace DribblyAPI.Repositories
{
    public class BaseRepository<T>: IDisposable, IBaseRepository<T> where T: BaseEntity
    {
        protected ApplicationDbContext ctx;
        protected readonly IDbSet<T> _dbset;

        public BaseRepository(ApplicationDbContext _ctx)
        {
            ctx = _ctx;
            _dbset = _ctx.Set<T>();
        }

        public T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save()
        {
            ctx.SaveChanges();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public T FindSingleBy(Expression<Func<T, bool>> predicate)
        {
            T entity = _dbset.FirstOrDefault(predicate);
            return entity;
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Count(predicate) > 0;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        public bool IsCurrentUserId(string userId)
        {
            try
            {
                string currUserId = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;
                return userId.ToLower() == currUserId.ToLower();
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public string GetCurrentUserId()
        {
            try
            {
                string currUserId = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;
                return currUserId;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Adds general information to log messages, such as userId
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public String FormatLogMessage(String message)
        {
            message = message + ", userId: " + GetCurrentUserId();
            return message;
        }

    }
}
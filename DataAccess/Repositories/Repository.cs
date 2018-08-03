using System;
using System.Collections.Generic;
using HomiesAPI.Models;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomiesAPI.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly HomiesContext _dbContext;

        protected DbSet<T> _dbSet;

        public Repository(HomiesContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual T GetById(int id, Expression<Func<T, object>>[] includes = null)
        {
            return Get(T => T.Id == id, includes);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(predicate);
        }

        public virtual List<T> List(Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public virtual List<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            
            return query.Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
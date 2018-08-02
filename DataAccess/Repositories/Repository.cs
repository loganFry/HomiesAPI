using System;
using System.Collections.Generic;
using HomiesAPI.Models;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HomiesAPI.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly HomiesContext _dbContext;

        public Repository(HomiesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> List()
        {
            return _dbContext.Set<T>();
        }

        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                   .Where(predicate)
                   .AsEnumerable();
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
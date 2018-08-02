using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HomiesAPI.DataAccess.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(int id, Expression<Func<T, object>>[] includes = null);
        T Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null);
        IEnumerable<T> List(Expression<Func<T, object>>[] includes = null);
        IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }

    public abstract class EntityBase
    {
        public int Id { get; protected set; }
    }
}
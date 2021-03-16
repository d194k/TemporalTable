using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EMP.Entities.Repositories
{
    public class Repository<T>: IRepository<T> where T: class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            var dbEntity = _context.Entry<T>(entity);
            dbEntity.State = EntityState.Added;
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public T Update(T entity)
        {
            var dbEntity = _context.Entry<T>(entity);
            dbEntity.State = EntityState.Modified;
            return entity;
        }
    }
}

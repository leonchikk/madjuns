using Common.Core.Interfaces;
using Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Auth.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AuthContext _dbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(AuthContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public T Add(T obj)
        {
            DbSet.Add(obj);
            return obj;
        }

        public async Task<T> AddAsync(T obj)
        {
            await DbSet.AddAsync(obj);
            return obj;
        }

        public void Delete(Guid id)
        {
            T obj = DbSet.First(x => x.Id == id);
            DbSet.Remove(obj);
        }

        public async Task DeleteAsync(Guid id)
        {
            T obj = await DbSet.FirstAsync(x => x.Id == id);
            DbSet.Remove(obj);
        }

        public bool Contains(T entity)
        {
            return DbSet.FirstOrDefault(t => t == entity) != null;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> FindBy<T2>(Expression<Func<T, bool>> predicate, params Expression<Func<T, T2>>[] paths)
        {
            foreach (Expression<Func<T, T2>> path in paths)
            {
                DbSet.Include(path);
            }

            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public T GetById(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

        public T Update(T obj)
        {
            DbSet.Update(obj);
            return obj;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> obj)
        {
            await DbSet.AddRangeAsync(obj);
            return obj;
        }

        public void Delete(T obj)
        {
            DbSet.Remove(obj);
        }

        public void DeleteRange(IEnumerable<T> obj)
        {
            DbSet.RemoveRange(obj);
        }

        public IQueryable<T> GetAllWithIncludies(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet.AsQueryable();
            foreach (Expression<Func<T, object>> include in includeProperties)
            {
                query = query.Include(include);
            }

            return query;
        }

        public T GetByIdWithIncludies(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet.Where(x => x.Id == id);
            foreach (Expression<Func<T, object>> include in includeProperties)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault();
        }
    }
}

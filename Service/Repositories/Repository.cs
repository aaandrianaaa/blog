using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class Repository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private readonly BlogContext DbContext;

        protected Repository(BlogContext dbContext)
        {
            this.DbContext = dbContext;
            this._dbSet = this.DbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(where);
            return entity;
        }

        public async  Task< List<T>> GetList(Expression<Func<T, bool>> where, int limit, int page )
        {
            return await _dbSet.Where(where).Skip(limit * page).Take(limit).ToListAsync();

        }

        public async Task<List<T>> GetListOfAll(int limit, int page)
        {
            return await _dbSet.Skip(limit * page).Take(limit).ToListAsync();

        }

        public async Task Update(T entity, Expression<Func<T, bool>> where)
        {
            await _dbSet.FirstOrDefaultAsync(@where);

            await DbContext.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }

    

        public async Task<List<T>> GetManyIncludingAllAsync(Expression<Func<T, bool>> where, int limit, int page)
        {
            var query = _dbSet.AsQueryable();

            query = DbContext.Model.FindEntityType(typeof(T)).GetNavigations().Aggregate(query, (current, property) => current.Include(property.Name));

            return await query.Where(where).Skip(limit * page).Take(limit).ToListAsync();
        }


        public T GetWithInclude(Expression<Func<T, bool>> where, params string[] include)
        {
            var query = _dbSet.AsQueryable();

            query = include.Aggregate(query, (current, property) => current.Include(property));

            return query.FirstOrDefault(where);
        }

        public  T GetIncludingAll(Expression<Func<T, bool>> where)
        {
            var query = _dbSet.AsQueryable();

            query = DbContext.Model.FindEntityType(typeof(T)).GetNavigations().Aggregate(query, (current, property) => current.Include(property.Name));

            return query.FirstOrDefault(where);
        }

        public List<T> GetManyIncludingAll(Expression<Func<T, bool>> where)
        {
            var query = _dbSet.AsQueryable();

            query = DbContext.Model.FindEntityType(typeof(T)).GetNavigations().Aggregate(query, (current, property) => current.Include(property.Name));

            return  query.Where(where).ToList();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();

        }
    }
}

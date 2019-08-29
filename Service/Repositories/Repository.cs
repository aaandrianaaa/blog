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
        protected readonly DbSet<T> dbSet;
        protected readonly BlogContext dbContext;

        public Repository(BlogContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            var entity = await dbSet.FirstOrDefaultAsync(where);
            return entity;
        }

        public async  Task< List<T>> GetList(Expression<Func<T, bool>> where, int limit, int page )
        {
            return await dbSet.Where(where).Skip(limit * page).Take(limit).ToListAsync();

        }

        public async Task<List<T>> GetListOfAll(int limit, int page)
        {
            return await dbSet.Skip(limit * page).Take(limit).ToListAsync();

        }

        public async Task Update(T entity, Expression<Func<T, bool>> where)
        {
           var find = await dbSet.FirstOrDefaultAsync(where);
            find = entity;
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    

        public async Task<List<T>> GetManyIncludingAllAsync(Expression<Func<T, bool>> where, int limit, int page)
        {
            var query = dbSet.AsQueryable();

            foreach (var property in dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return await query.Where(where).Skip(limit * page).Take(limit).ToListAsync();
        }


        public T GetWithInclude(Expression<Func<T, bool>> where, params string[] include)
        {
            var query = dbSet.AsQueryable();

            foreach (var property in include)
                query = query.Include(property);

            return query.FirstOrDefault(where);
        }

        public  T GetIncludingAll(Expression<Func<T, bool>> where)
        {
            var query = dbSet.AsQueryable();

            foreach (var property in dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return query.FirstOrDefault(where);
        }

    }
}

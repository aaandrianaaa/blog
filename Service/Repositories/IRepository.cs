using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        void Delete(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        Task<List<T>> GetList(Expression<Func<T, bool>> where, int limit, int page);
        Task<List<T>> GetListOfAll(int limit, int page);
        Task Update(T entity, Expression<Func<T, bool>> where);
        Task SaveAsync();
        Task<List<T>> GetManyIncludingAllAsync(Expression<Func<T, bool>> where, int limit, int page);
        T GetWithInclude(Expression<Func<T, bool>> where, params string[] include);
        T GetIncludingAll(Expression<Func<T, bool>> where);
       List<T> GetManyIncludingAll(Expression<Func<T, bool>> where);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> where);

    }
}

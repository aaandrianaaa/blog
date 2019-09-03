using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{

    public interface ILikeRepository : IRepository<Like>
    {
        List<User> GetUsers(Expression<Func<Like, bool>> where);
    }

    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        public LikeRepository(BlogContext dbContext) : base(dbContext)
        {


        }
    
        public List<User> GetUsers(Expression<Func<Like, bool>> where)
        {
            var users = _dbSet.Include(x => x.User).Where(where).Select(x => x.User).ToList();
            return users;
        }
    }
}

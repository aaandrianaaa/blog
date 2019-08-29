using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BlogContext dbContext) : base(dbContext)
        {
         
                
        }
    }
}

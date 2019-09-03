using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        
    }

    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext dbContext) : base(dbContext)
        {
      
        }

    }
}

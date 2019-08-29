using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {

    }

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogContext dbContext) : base(dbContext)
        {


        }
    }
}

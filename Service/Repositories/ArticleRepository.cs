using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {

    }

    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(BlogContext dbContext) : base(dbContext)
        {


        }
    }
}

using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repositories
{
    public interface ISavedArticlesRepository : IRepository<SavedArticles>
    {

    }

    public class SavedArticlesRepository : Repository<SavedArticles>, ISavedArticlesRepository
    {
        public SavedArticlesRepository(BlogContext dbContext) : base(dbContext)
        {


        }
    }
}

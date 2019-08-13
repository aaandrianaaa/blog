using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
   public interface IArticleService
    {
 
        Task<Article> GetByIDAsync (int id);
        List<Article> GetList(string orderBy);
        Task<bool> DeleteByIDAsync(int id);
        Task<bool> CreateAsync(Article article, int id);
        Task<bool> PatchAsync(int id, Article Article);
        List<Article> GetByCategoryID(int category_id, string orderBy);
        Task<bool> RatingArticle(int id, int rating);

         List<SavedArticles> GetSavedArticles(int id);

        Task<bool> SaveArticle(int user_id, int article_id);
    }
}

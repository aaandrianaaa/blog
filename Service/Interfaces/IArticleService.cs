using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IArticleService
    {
        Task<Article> GetByIDAsync(int id);
        Task<List<Article>> GetList( int limit, int page);
        Task<bool> DeleteByIDAsync(int id, int user_id);
        Task<bool> CreateAsync(Article article, int id);
        Task<bool> PatchAsync(int id, Article Article, int user_id);
        Task<List<Article>> GetByCategoryID(int category_id, int limit, int page);
        Task<bool> RatingArticle(int id, int rating);
        Task<List<SavedArticles>> GetSavedArticles(int id, int limit, int page);
        Task<bool> SaveArticle(int user_id, int article_id);
        Task<List<Article>> ArticlesByThisAuthor(int id,int limit, int page);
    }
}

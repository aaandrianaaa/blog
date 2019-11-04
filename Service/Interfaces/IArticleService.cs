using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IArticleService
    {
        Task<Article> GetByIdAsync(int id);
        Task<List<Article>> GetList( int limit, int page);
        Task<bool> DeleteByIdAsync(int id, int userId);
        Task<bool> CreateAsync(Article article, int id);
        Task<bool> PatchAsync(int id, Article article, int userId);
        Task<List<Article>> GetByCategoryIdAsync(int categoryId, int limit, int page);
        Task<bool> RatingArticleAsync(int id, int rating);
        Task<List<SavedArticles>> GetSavedArticlesAsync(int id, int limit, int page);
        Task<bool> SaveArticle(int userId, int articleId);
        Task<List<Article>> ArticlesByThisAuthor(int id,int limit, int page);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.Interfaces;
using Service.Models;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ArticleService : IArticleService
    {
        readonly IArticleRepository articleRepository;
        readonly IUserRepository userRepository;
        readonly ICategoryRepository categoryRepository;
        readonly ISavedArticlesRepository savedArticlesRepository;

        public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, ISavedArticlesRepository savedArticlesRepository)
        {
            this.articleRepository = articleRepository;
            this.userRepository = userRepository;
            this.categoryRepository = categoryRepository;
            this.savedArticlesRepository = savedArticlesRepository;
        }



        public async Task<Article> GetByIDAsync(int id)
        {
            var article = articleRepository.GetIncludingAll(a => a.Deleted_at == null && a.ID == id);
            if (article == null) return null;
            article.ViewCount++;
            await articleRepository.SaveAsync();
            return article;
        }

        public async Task<List<Article>> GetList(int limit, int page)
        {
            var article = await articleRepository.GetManyIncludingAllAsync(a => a.Deleted_at == null, limit, page);
            return article;

        }

        public async Task<bool> DeleteByIDAsync(int id, int user_id)
        {
            var user = await userRepository.GetAsync(x => x.ID == user_id);
            var article = await articleRepository.GetAsync(x => x.ID == id);
            if (article != null && (article.AuthorID == user_id || user.RoleID == Roles.User || user.RoleID == 4))
            {
                article.Deleted_at = DateTime.Now;
                var category = await categoryRepository.GetAsync(x => x.ID == article.CategoryID);
                if (category == null) return false;
                category.ArticleCount--;
                await articleRepository.SaveAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CreateAsync(Article article, int id)
        {
            article.AuthorID = id;
            await articleRepository.CreateAsync(article);
            var category = await categoryRepository.GetAsync(x => x.ID == article.CategoryID);
            if (category == null) return false;
            category.ArticleCount++;
            await articleRepository.SaveAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int id, Article Article, int user_id)
        {
            var article = await articleRepository.GetAsync(a => a.ID == id && a.Deleted_at == null);
            var user = await userRepository.GetAsync(x => x.ID == user_id);
            if (article != null && (article.AuthorID == user_id || user.RoleID == 4))
            {
                var new_article = await articleRepository.GetAsync(x => x.ID == id);
                if (Article.Name != null)
                    new_article.Name = Article.Name;
                if (Article.Text != null)
                    new_article.Text = Article.Text;
                await articleRepository.Update(new_article, (x => x.ID == id));
                await articleRepository.SaveAsync();
                return true;
            }

            return false;
        }


        public async Task<List<Article>> GetByCategoryID(int category_id, int limit, int page)
        {
            var article = await articleRepository.GetManyIncludingAllAsync((a => a.CategoryID == a.Category.ID && a.Deleted_at == null && a.Author.ID == a.AuthorID), limit, page);
            return article;
        }


        public async Task<bool> RatingArticle(int id, int rating)
        {
            var article = await articleRepository.GetAsync(a => a.ID == id && a.Deleted_at == null);
            if (article == null || rating > 5 && rating < 0) return false;

            if (article.Rating > 0)
                article.Rating = (article.Rating + rating) / 2;
            if (article.Rating == 0)
                article.Rating = article.Rating + rating;

            await articleRepository.SaveAsync();
            return true;


        }

        public async Task<List<SavedArticles>> GetSavedArticles(int id, int limit, int page)
        {
            var articles = await savedArticlesRepository.GetManyIncludingAllAsync((a => a.UsreID == id && a.User.ID == a.UsreID && a.ArticleID == a.Article.ID && a.Article.AuthorID == a.Article.Author.ID), limit, page);

            //  .Where(a => a.UsreID == id)
            //.Include(a => a.User)
            //.Where(u => u.User.ID == u.UsreID)
            // .Include(a => a.Article)
            // .Where(a => a.ArticleID == a.Article.ID)
            // .Include(a => a.Article.Author)
            // .AsQueryable();

            return articles;
        }

        public async Task<bool> SaveArticle(int user_id, int article_id)
        {
            var save = new SavedArticles()
            {
                ArticleID = article_id,
                UsreID = user_id
            };

            await savedArticlesRepository.CreateAsync(save);
            await savedArticlesRepository.SaveAsync();

            return true;
        }

        public async Task<List<Article>> ArticlesByThisAuthor(int id, int limit, int page)
        {
            var articles = await articleRepository.GetManyIncludingAllAsync((a => a.AuthorID == id && a.Author.ID == a.AuthorID && a.Category.ID == a.CategoryID), limit, page);
            return articles;
        }
    }


}



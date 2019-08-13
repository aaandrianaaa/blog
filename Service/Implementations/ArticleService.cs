using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ArticleService : IArticleService
    {
        BlogContext db;

        public ArticleService(BlogContext db)
        {
            this.db = db;
        }



        public async Task<Article> GetByIDAsync(int id)
        {
            if (db.Articles.Where(a => a.Deleted_at == null)
                .Where(a => a.ID == id)
                .FirstOrDefault() != null)
            {
                IQueryable<Article> IQarticle = db.Articles
                    .Where(a => a.ID == id)
                    .Include(a => a.Category)
                    .Where(a => a.CategoryID == a.Category.ID)
                    .Include(a => a.Author)
                    .Where(a => a.Author.ID == a.AuthorID);
                var article = await IQarticle.FirstOrDefaultAsync<Article>();
                article.ViewCount++;
                await db.SaveChangesAsync();
                return article;
            }
            return null;
        }

        public List<Article> GetList(string orderBy)
        {
            var article = db.Articles
                  .Include(a => a.Category)
                  .Where(a => a.CategoryID == a.Category.ID)
                  .Include(a=> a.Author)
                  .Where(a=> a.Author.ID == a.AuthorID)
                  .Where(a => a.Deleted_at == null)
                  .AsQueryable();


            switch (orderBy)
            {
                case "rating asc":
                    article = article.OrderBy(x => x.Rating);
                    break;
                case "rating desc":
                    article = article.OrderByDescending(x => x.Rating);
                    break;
                case "view asc":
                    article = article.OrderBy(x => x.ViewCount);
                    break;

                case "view desc":
                    article = article.OrderByDescending(x => x.ViewCount);
                    break;

                case "created at":
                    article = article.OrderBy(x => x.Created_at);
                    break;
            }

            return article.ToList();

        }

        public async Task<bool> DeleteByIDAsync(int id)
        {

            var article = await db.Articles.FindAsync(id);
            if (article != null)
            {
                article.Deleted_at = DateTime.Now;
                
                var category = db.Categories.FirstOrDefault(x => x.ID == article.CategoryID);
                if (category == null)
                {
                    return false;

                }
                category.ArticleCount--;
                
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CreateAsync(Article article, int id)
        {
            article.AuthorID = id;
           await db.AddAsync(article);

            var category = db.Categories.FirstOrDefault(x => x.ID == article.CategoryID);
            if (category == null)
            {
                return false;

            }
            category.ArticleCount++;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int id, Article Article)
        {
            var article = db.Articles
                .Where(x => x.ID == id)
                .Where(a => a.Deleted_at == null)
                .FirstOrDefault();

            if (article != null)
            {
                var new_article = db.Articles.Where(x => x.ID == id).First();
                if (Article.Name != null)
                    new_article.Name = Article.Name;
                if (Article.Text != null)
                    new_article.Text = Article.Text;
                db.Articles.Update(new_article);
                await db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public List<Article> GetByCategoryID(int category_id, string orderBy)
        {

            var article = db.Articles.Where(x => x.CategoryID == category_id)
                 .Include(a => a.Category).Where(a => a.CategoryID == a.Category.ID)
                 .Where(a => a.Deleted_at == null)
                 .Include(a => a.Author)
                  .Where(a => a.Author.ID == a.AuthorID)
                .AsQueryable();


            switch (orderBy)
            {
                case "rating asc":
                    article = article.OrderBy(x => x.Rating);
                    break;
                case "rating desc":
                    article = article.OrderByDescending(x => x.Rating);
                    break;

                case "view asc":
                    article = article.OrderBy(x => x.ViewCount);
                    break;
                case "view desc":
                    article = article.OrderByDescending(x => x.ViewCount);
                    break;
                case "created at":
                    article = article.OrderBy(x => x.Created_at);
                    break;
            }

            return article.ToList();
        }

        public async Task<bool> RatingArticle(int id, int rating)
        {
            var article = db.Articles.Where(a => a.ID == id).Where(a => a.Deleted_at == null).FirstOrDefault();
            if (article != null)
            {
                if (rating <= 5 && rating > 0)
                {
                    if (article.Rating > 0)
                        article.Rating = (article.Rating + rating) / 2;
                    if (article.Rating == 0)
                        article.Rating = article.Rating + rating;

                    await db.SaveChangesAsync();
                    return true;

                }
            }

            return false;

        }

        public List<SavedArticles> GetSavedArticles(int id)
        {
            var articles = db.SavedArticles
                .Where(a => a.UsreID == id)
                .Include(a => a.Article)
                .Where(a => a.ArticleID == a.Article.ID)
                .Include(a => a.Article.Author);
                
            return articles.ToList();
        }

        public async Task<bool> SaveArticle(int user_id, int article_id)
        {
            var save = new SavedArticles()
            {
                ArticleID = article_id,
                UsreID = user_id
            };

            await db.AddAsync(save);
            await db.SaveChangesAsync();

            return true;
        }

    }


}



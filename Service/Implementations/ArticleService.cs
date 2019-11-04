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
       private readonly IArticleRepository _articleRepository;
       private readonly IUserRepository _userRepository;
       private readonly ICategoryRepository _categoryRepository;
       private readonly ISavedArticlesRepository _savedArticlesRepository;
       private readonly ICommentRepository _commentRepository;
        private readonly IImageRepository _imageRepository;
       

        public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, ISavedArticlesRepository savedArticlesRepository, ICommentRepository commentRepository, IImageRepository imageRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _savedArticlesRepository = savedArticlesRepository;
            _commentRepository = commentRepository;
            _imageRepository = imageRepository;
          
        }



        public async Task<Article> GetByIdAsync(int id)
        {
            var article =  _articleRepository.GetIncludingAll(a => a.DeletedAt == null && a.ID == id);
            if (article == null) return null;
            article.ViewCount++;
            await _articleRepository.SaveAsync();
            article.Author.Avatar = await _imageRepository.GetAvatarAsync(article.AuthorID);
            return article;
        }

        public async Task<List<Article>> GetList(int limit, int page)
        {
            var articles = await _articleRepository.GetManyIncludingAllAsync(a => a.DeletedAt == null, limit, page);
            foreach (var article in articles) article.Author.Avatar = await _imageRepository.GetAvatarAsync(article.AuthorID);
            return articles;

        }

        public async Task<bool> DeleteByIdAsync(int id, int userId)
        {
            var user = await _userRepository.GetAsync(x => x.ID == userId);
            var article = await _articleRepository.GetAsync(x => x.ID == id);
            if (article == null || (article.AuthorID != userId && user.RoleID != Roles.User && user.RoleID != 4))
                return false;
            {
                article.DeletedAt = DateTime.Now;
                var category = await _categoryRepository.GetAsync(x => x.ID == article.CategoryID);
                if (category == null) return false;
                category.ArticleCount--;
                await _articleRepository.SaveAsync();
                return true;
            }

        }

        public async Task<bool> CreateAsync(Article article, int id)
        {
            article.AuthorID = id;
            await _articleRepository.CreateAsync(article);
            var category = await _categoryRepository.GetAsync(x => x.ID == article.CategoryID);
            if (category == null) return false;
            category.ArticleCount++;
            await _articleRepository.SaveAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int id, Article Article, int userId)
        {
            var article = await _articleRepository.GetAsync(a => a.ID == id && a.DeletedAt == null);
            var user = await _userRepository.GetAsync(x => x.ID == userId);
            if (article == null || (article.AuthorID != userId && user.RoleID != 4)) return false;
            {
                var newArticle = await _articleRepository.GetAsync(x => x.ID == id);
                if (Article.Name != null)
                    newArticle.Name = Article.Name;
                if (Article.Text != null)
                    newArticle.Text = Article.Text;
                await _articleRepository.Update(newArticle, (x => x.ID == id));
                await _articleRepository.SaveAsync();
                return true;
            }

        }


        public async Task<List<Article>> GetByCategoryIdAsync(int categoryId, int limit, int page)
        {
            var articles = await _articleRepository.GetManyIncludingAllAsync((a => a.CategoryID == a.Category.ID && a.DeletedAt == null && a.Author.ID == a.AuthorID), limit, page);
            foreach (var article in articles) article.Author.Avatar = await _imageRepository.GetAvatarAsync(article.AuthorID);
            return articles;
        }


        public async Task<bool> RatingArticleAsync(int id, int rating)
        {
            var article = await _articleRepository.GetAsync(a => a.ID == id && a.DeletedAt == null);
            if (article == null || rating > 5 && rating < 0) return false;

            if (article.Rating > 0)
                article.Rating = (article.Rating + rating) / 2;
            if (article.Rating < 0)
                article.Rating += rating;

            await _articleRepository.SaveAsync();
            return true;


        }

        public async Task<List<SavedArticles>> GetSavedArticlesAsync(int id, int limit, int page)
        {
            var articles = await _savedArticlesRepository.GetManyIncludingAllAsync((a => a.UsreID == id && a.User.ID == a.UsreID && a.ArticleID == a.Article.ID && a.Article.AuthorID == a.Article.Author.ID), limit, page);
            foreach (var article in articles) article.Article.Author.Avatar = await _imageRepository.GetAvatarAsync(article.Article.AuthorID);
            return articles;
        }

        public async Task<bool> SaveArticle(int userId, int articleId)
        {
            var save = new SavedArticles()
            {
                ArticleID = articleId,
                UsreID = userId
            };

            await _savedArticlesRepository.CreateAsync(save);
            await _savedArticlesRepository.SaveAsync();

            return true;
        }

        public async Task<List<Article>> ArticlesByThisAuthor(int id, int limit, int page)
        {
            var articles = await _articleRepository.GetManyIncludingAllAsync((a => a.AuthorID == id && a.Author.ID == a.AuthorID && a.Category.ID == a.CategoryID), limit, page);
            foreach (var article in articles) article.Author.Avatar = await _imageRepository.GetAvatarAsync(article.AuthorID);
            return articles;
        }

        
    }


}



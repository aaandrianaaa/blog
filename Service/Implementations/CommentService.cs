using Service.Helper;
using Service.Interfaces;
using Service.Models;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class CommentService : ICommentService

    {
        BlogContext db;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;
        public CommentService(BlogContext db, ICommentRepository commentRepository, ILikeRepository likeRepository, IUserRepository userRepository)
        {
            this.db = db;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Create(int articleId, int userId, string text)
        {

            var coment = new Comment
            {
                ArticleID = articleId,
                UserID = userId,
                Text = text,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            };
            await db.Comments.AddAsync(coment);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Comment>> GetByArticleIdAsync(int id, int limit, int page)
        {
            var comments = await _commentRepository.GetManyIncludingAllAsync(c => c.ArticleID == id && c.DeletedAt == null, limit, page);
            return comments;
        }

        public async Task<bool> UpdateCommentAsync(int id, string text, int userId)
        {
            var comment = await _commentRepository.GetAsync(c => c.ID == id && c.User.ID == userId && c.DeletedAt == null);
            if (comment == null || text == null) return false;
            comment.Text = text;
            comment.UpdatedAt = DateTime.Now;
            await _commentRepository.Update(comment, c => c.ID == id);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id, int userId)
        {
            var user = await _userRepository.GetAsync(u => u.ID == userId);
            var comment = await _commentRepository.GetAsync(c => c.ID == id && c.DeletedAt == null && (c.UserID == userId || user.RoleID == Roles.Admin ||user.RoleID==Roles.Moderator ));
            if (comment == null) return false;
            comment.DeletedAt = DateTime.Now;
            await _commentRepository.SaveAsync();
            return true;
        }

        public async Task<bool> LikeAndDisLikeAsync(int CommentId, int UserId)
        {
            var comment = await _commentRepository.GetAsync(c => c.ID == CommentId);
            var user = await _userRepository.GetAsync(u => u.ID == UserId);
            if (comment == null || user == null) return false;
            var like = await _likeRepository.GetAsync(l => l.CommentId == CommentId && l.UserId == UserId);
    
            if (like == null)
            {
                await _likeRepository.CreateAsync(new Like() {
                    UserId = UserId,
                    CommentId = CommentId,
                    LikeIt = true,
                });
                comment.Likes++;
                await _likeRepository.SaveAsync();
                return true;
            }
            if (like.LikeIt)
            {
                like.LikeIt = false;
                comment.Likes--;
                await _likeRepository.SaveAsync();
                return true;
            }
            if (!like.LikeIt)
            {
                like.LikeIt = true;
                comment.Likes++;
                await _likeRepository.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> ListUserLikesAsync (int CommentId)
        {
         
            var users =  _likeRepository.GetUsers(x => x.CommentId == CommentId && x.LikeIt == true);
            return users;
        }

    }
}

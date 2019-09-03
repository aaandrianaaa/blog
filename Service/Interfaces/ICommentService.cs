using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICommentService
    {
        Task<bool> Create(int article_id, int user_id, string text);
        Task<List<Comment>> GetByArticleIdAsync(int id, int limit, int page);
        Task<bool> UpdateCommentAsync(int id, string text, int userId);
        Task<bool> DeleteByIdAsync(int id, int userId);
        Task<bool> LikeAndDisLikeAsync(int CommentId, int UserId);
        Task<List<User>> ListUserLikesAsync(int CommentId);
    }
}

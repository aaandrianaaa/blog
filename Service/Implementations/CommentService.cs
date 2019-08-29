using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class CommentService : ICommentService
    {
        BlogContext db;
        public CommentService(BlogContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create (int article_id, int user_id, string text)
        {
            
            var coment = new Comment
            {
                ArticleID = article_id,
                UserID = user_id,
                Text = text,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            };
            await db.Comments.AddAsync(coment);
            await db.SaveChangesAsync();

            return true;
        }
    }
}

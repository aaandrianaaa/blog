using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICommentService
    {
        Task<bool> Create(int article_id, int user_id, string text);
    }
}

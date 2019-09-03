using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class CommentsView
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int userId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Service.Models
{

    [Table(name: "comments")]
    public class Comment: Base
    {
      
        [Column(name: "article_id")]
        public int ArticleID { get; set; }
        [Column(name: "user_id")]
        public int UserID { get; set; }
        [Column(name: "user")]
        public User User { get; set; }
        [Column(name: "text")]
        public string Text { get; set; }
        [Column(name: "likes")]
        public int Likes { get; set; } = 0;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Service.Models
{

    [Table (name: "comments")]
    public class Comment
    {
        [Column(name: "id")]
        public int ID { get; set; }
        [Column(name: "article_id")]
        public int ArticleID { get; set; }
        [Column(name: "user_id")]
        public int UserID { get; set; }
        [Column(name: "user")]
        public User User { get; set; }
        [Column (name: "text")]
        public string Text { get; set; }
        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }
        [Column(name: "updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column(name: "deleted_at")]
        public DateTime? DeletedAt { get; set; }
        
    }
}

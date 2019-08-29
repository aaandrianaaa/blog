using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Service.Models
{
    [Table("saved_articles")]
   public class SavedArticles
    {
        [Column(name: "id")]
        public int ID { get; set; }

        [Column(name: "article_id")]
       [ForeignKey("Article")]
        public int ArticleID { get; set; }

        [Column(name: "user_id")]
        [ForeignKey("User")]
        public int UsreID { get; set; }

        [Column(name: "articles")]
        public Article Article { get; set; }

        [Column(name: "user")]
        public User User { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Service.Models
{
    [Table("articles")]
    public class Article : Base
    {
     
        [Column(name: "text")]
        public string Text { get; set; }
        [Column(name: "name")]
        public string Name { get; set; }
        [Column(name: "view_count")]
        public int ViewCount { get; set; } = 0;
        [Column(name: "category_id")]
        public int CategoryID { get; set; }
        [Column(name: "category")]
        public Category Category { get; set; }
        [Column(name: "rating")]
        public double Rating { get; set; } = 0;
        [Column (name:"author_id")]
        public int AuthorID { get; set; }
        [Column(name: "author")]
        public User Author { get; set; }
        public List<Comment> Comment { get; set; }

    }
}

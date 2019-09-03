using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Models
{
    [Table("categories")]
    public class Category: Base
    {
      
        [Column(name: "name")]
        public string Name { get; set; }
        [Column(name: "article_count")]
        public int ArticleCount { get; set; } = 0;
        [Column(name: "description")]
        public string Description { get; set; } = "No description";
        [Column(name: "view_count")]
        public int ViewCount { get; set; } = 0;


    }    
}

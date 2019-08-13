using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Models
{
    [Table("categories")]
    public class Category
    {
        [Column(name: "id")]
        [ForeignKey("CategoryID")]
        public int ID { get; set; }
        [Column(name: "name")]
        public string Name { get; set; }
        [Column(name: "article_count")]
        public int ArticleCount { get; set; } = 0;
        [Column(name: "description")]
        public string Description { get; set; } = "No description";
        [Column(name: "view_count")]
        public int ViewCount { get; set; } = 0;
        [Column(name: "deleted_at")]
        public DateTime? Deleted_at { get; set; }
        [Column(name: "category_id")]
        public DateTime Created_at { get; set; }
        [Column(name: "updated_at")]
        public DateTime Updated_at { get; set; }



    }    
}

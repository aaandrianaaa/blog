using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Models;

namespace WebApi.ViewModel
{
    public class ArticleView
    {
     

        public int ID { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

        public int ViewCount { get; set; }

        public int CategoryID { get; set; }

        public CategoriesView Category { get; set; }

        public double Rating { get; set; }

        public int AuthorID { get; set; }

        public UsersView Author { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}

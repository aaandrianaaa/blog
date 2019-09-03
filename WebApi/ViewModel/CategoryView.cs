using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class CategoryView
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int ArticleCount { get; set; }

        public string Description { get; set; }
        public int ViewCount { get; set; }
       
        public DateTime CreatedAt { get; set; }
       
    
    }
}

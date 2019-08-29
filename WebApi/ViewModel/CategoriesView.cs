using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.ViewModel
{
    public class CategoriesView
    {
    
        public int ID { get; set; }
        
        public string Name { get; set; }
    
        public int ArticleCount { get; set; } 
      
        public string Description { get; set; } 
        public int ViewCount { get; set; } 
      
       

    }
}

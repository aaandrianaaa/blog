using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class SavedArticleView
    {
   
        public int ID { get; set; }

        public ArticlesView Article { get; set; }
        public int AuthorID { get; set; }
        public UsersView Author { get; set; }
    }
}

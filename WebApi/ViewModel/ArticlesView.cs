using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.ViewModel
{
    public class ArticlesView
    {

        public int ID { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

        public int ViewCount { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public double Rating { get; set; }

        public int AuthorID { get; set; }

        public string AuthorNickname { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

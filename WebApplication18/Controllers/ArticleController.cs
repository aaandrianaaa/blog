//using Microsoft.AspNetCore.Mvc;
//using Service.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WebApplication18.Models;
//using WebApplication18.Requests;

//namespace WebApplication18.Controllers
//{
//    [Route("articles")]
//    public class ArticleController : Controller
//    {
//        private BlogContext db;

//        public ArticleController(BlogContext db)
//        {
//            this.db = db;
//        }

//        [HttpGet("create/{id}")]
//        public IActionResult Create(int id)
//        {
//            var article = new Article();
//            article.CategoryID = id;
//            return View(article);
//        }

//        [HttpPost("create")]
//        public IActionResult Create(CreateArticleRequest request)
//        {
//            var article = new Article();
//            article.CategoryID = request.CategoryID;
//            article.Name = request.Name;
//            article.Text = request.Text;
//            db.Articles.Add(article);
//            db.SaveChanges();
//            return Redirect("/");
//        }

//        [HttpGet("/")]
//        public IActionResult Index()
//        {
//            var list = db.Articles.ToList();
//            return View(list);
//        }

//        [HttpGet("category/{id}")]
//public IActionResult CategoryArticles(int id)
//{
//    var articles = db.Articles.Where(x => x.CategoryID == id).ToList();
//    return View(articles);
//}

//        [HttpGet("{id}")]
//        public IActionResult SelectArticle(int id)
//        {
//            var article = db.Articles.Where(x => x.ID == id).First();
//            article.ViewCount++;
//            db.SaveChanges();
//            return View(article);
//        }
//        [HttpGet("delete/{id}")]
//        public IActionResult Delete(int id)
//        {
//            var article = db.Articles.Find(id);
//            if (article == null)
//            {
//                return StatusCode(404);
//            }
//            db.Articles.Remove(article);
//            db.SaveChanges();

//            return Redirect("/articles");
//        }

//        [HttpGet("top")]
//        public IActionResult Top()
//        {
//            var articles = db.Articles.OrderByDescending(p => p.Count).ToList();
//            return View(articles);
//        }

//    }
//}
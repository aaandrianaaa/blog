//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Service.Models;
//using WebApplication18.Models;


//namespace WebApplication18.Controllers
//{
//    [Route("category")]
//    public class CategoryController : Controller
//    {
//        private BlogContext db;

//        public CategoryController(BlogContext db)
//        {
//            this.db = db;
//        }

//        [HttpGet("create")]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost("create")]
//        public IActionResult Create(string Name)
//        {
//            //Category b = new Category(Name);
//            //db.Categories.Add(b);
//            //db.SaveChanges();

//            //return Redirect("/articles/create/" + b.ID.ToString());

//            return Ok();
//        }

//        [HttpGet("delete/{id}")]
//        public IActionResult Delete(int id)
//        {
//            Category b = db.Categories.Find(id);
//            if (b == null)
//            {       
//                return HttpNotFound();
//            }
//            var article = db.Articles.Where(x => x.CategoryID == id).ToList();
//            if (article.Count == 0)
//            {
//                db.Categories.Remove(b);
//                db.SaveChanges();
//            }
//            return Redirect("/category");
//        }

//        private IActionResult HttpNotFound()
//        {
//            return Redirect("/");
//        }

//        [HttpGet("")]
//        public IActionResult GetList()
//        {
//            var list = db.Categories.ToList();
//            return View(list);
//        }

//        [HttpGet("list")]
//        public IActionResult SelectCategory()
//        {
//            var list = db.Categories.ToList();
//            return View(list);
//        }
//    }
//}


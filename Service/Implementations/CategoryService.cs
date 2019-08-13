using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
   public class CategoryService : ICategoryService
    {
        BlogContext db;

        public CategoryService(BlogContext db)
        {
            this.db = db;
        }

 

        public async Task <Category> GetByIDAsync (int id)
        {
            var category =  db.Categories.Where(c => c.Deleted_at == null).Where(c=> c.ID ==id).FirstOrDefault();
            if (category == null)
                return null;
            category.ViewCount++;
            await db.SaveChangesAsync();
            return category;
            
        }

        public async Task<bool> CreateAsync(Category category)
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteByIDAsync (int id)
        {
            var category = await db.Categories.FindAsync(id);

            if (category != null && db.Articles.ToList().Count(x => x.CategoryID == id) == 0)
            {
                category.Deleted_at = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public  List<Category> GetList(string orderBy)
        {
            var categories = db.Categories.Where(c => c.Deleted_at == null).AsQueryable();
            switch (orderBy)
            {
                case "view asc":
                    categories = categories.OrderBy(x => x.ViewCount);
                    break;

                case "view desc":
                    categories = categories.OrderByDescending(x => x.ViewCount);
                    break;

                case "article asc":
                    categories = categories.OrderBy(x => x.ArticleCount);
                    break;

                case "article desc":
                    categories = categories.OrderByDescending(x => x.ArticleCount);
                    break;
            }

            return categories.ToList();
        }

        public async Task<bool> PatchAsync(Category new_category, int id)
        {
            var category = db.Categories.Where(c => c.ID == id)
                .Where(c => c.Deleted_at == null)
                .FirstOrDefault();
            if(category != null)
            {
                if (new_category.Name != null)
                    category.Name = new_category.Name;
                if (new_category.Description != null)
                    category.Description = new_category.Description;
               await  db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}

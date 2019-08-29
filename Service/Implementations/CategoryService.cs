using Service.Interfaces;
using Service.Models;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Category> GetByIDAsync(int id)
        {
            var category = await categoryRepository.GetAsync(c => c.Deleted_at == null && c.ID == id);
            if (category == null) return null;
            category.ViewCount++;
            await categoryRepository.SaveAsync();
            return category;

        }

        public async Task<bool> CreateAsync(Category category)
        {
            await categoryRepository.CreateAsync(category);
            await categoryRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteByIDAsync(int id)
        {
            var category = await categoryRepository.GetAsync(c => c.ID == id);

            if (category == null || category.ArticleCount != 0) return false;
            category.Deleted_at = DateTime.Now;
            await categoryRepository.SaveAsync();
            return true;
        }

        public async Task<List<Category>> GetList(int limit, int page)
        {
            var categories = await categoryRepository.GetList((c => c.Deleted_at == null), limit, page);
            return categories;
        }

        public async Task<bool> PatchAsync(Category new_category, int id)
        {
            var category = await categoryRepository.GetAsync(c => c.ID == id && c.Deleted_at == null);
            if (category == null) return false;

            if (new_category.Name != null)
                category.Name = new_category.Name;
            if (new_category.Description != null)
                category.Description = new_category.Description;

            await categoryRepository.SaveAsync();
            return true;

        }
    }
}

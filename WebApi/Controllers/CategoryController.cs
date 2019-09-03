using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using WebApi.Requests;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("v1/api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
       private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            
            this._categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        { 

          var category = await _categoryService.GetByIDAsync(id);
            if (category == null) return BadRequest();

            var mapCategory = AutoMapper.Mapper.Map<CategoryView>(category);
            return Ok(mapCategory);
          
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost("")]
        public async Task <IActionResult> Create(CreateCategoryRequest request)
        {
            var mapCategory = AutoMapper.Mapper.Map<Category>(request);
            if (!await _categoryService.CreateAsync(mapCategory))
                return BadRequest();
        
            return Ok();
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {

            if (!await _categoryService.DeleteByIDAsync(id)) return BadRequest();
         
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task <IActionResult> List([FromQuery]Paginating request)
        {
            var categories = await _categoryService.GetList( request.Limit, request.Page);
           var mapCategory = AutoMapper.Mapper.Map<List<CategoriesView>>(categories);
            return Ok(mapCategory);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(PatchCategoryRequest request, int id)
        {
            var category = AutoMapper.Mapper.Map<Category>(request);
            if (await _categoryService.PatchAsync(category, id))
                return Ok();
            return BadRequest();
        }
    }
}
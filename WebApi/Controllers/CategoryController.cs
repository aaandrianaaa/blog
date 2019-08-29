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
        
        ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        { 

          var category = await categoryService.GetByIDAsync(id);
            if (category == null) return BadRequest();

            var _mapCategory = AutoMapper.Mapper.Map<CategoryView>(category);
            return Ok(_mapCategory);
          
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost("")]
        public async Task <IActionResult> Create(CreateCategoryRequest request)
        {
            var mapcategory = AutoMapper.Mapper.Map<Category>(request);
            if (!await categoryService.CreateAsync(mapcategory))
                return BadRequest();
        
            return Ok();
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
        

          if(!await categoryService.DeleteByIDAsync(id))
            {
                return BadRequest();
            }

            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task <IActionResult> List([FromQuery]Paginating request)
        {
            var categories = await categoryService.GetList( request.Limit, request.Page);
           var _mapCategory = AutoMapper.Mapper.Map<List<CategoriesView>>(categories);
            return Ok(_mapCategory);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(PatchCategoryRequest request, int id)
        {
            var category = AutoMapper.Mapper.Map<Category>(request);
            if (await categoryService.PatchAsync(category, id))
                return Ok();
            return BadRequest();
        }
    }
}
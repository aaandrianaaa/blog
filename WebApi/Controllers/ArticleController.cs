using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Service.Interfaces;
using Service.Models;
using WebApi.Requests;
using WebApi.ViewModel;
using Service.Extensions;
namespace WebApi.Controllers
{
    [Route("v1/api/articles")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {

            this._articleService = articleService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article == null) return BadRequest();

            var viewArticle = AutoMapper.Mapper.Map<ArticleView>(article);

            return Ok(viewArticle);
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> List([FromQuery]Paginating request)
        {
            var articles = await _articleService.GetList(request.Limit, request.Page);
            var mapArticles = AutoMapper.Mapper.Map<List<ArticlesView>>(articles);

            return Ok(mapArticles);
        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();
            if (!await _articleService.DeleteByIdAsync(id, userId.Value)) return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleRequestcs request)
        {
            var mapArticle = AutoMapper.Mapper.Map<Article>(request);
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();

            if (!await _articleService.CreateAsync(mapArticle, userId.Value))
                return NotFound();

            return Ok();

        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, PatchArticleRequest request)
        {
            var mapArticle = AutoMapper.Mapper.Map<Article>(request);
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();
            if (!await _articleService.PatchAsync(id, mapArticle, userId.Value))
                return BadRequest();
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("category/{category_id}")]
        public async Task<IActionResult> GetByCategory(int categoryId, [FromQuery]Paginating request)
        {

            var articles = await _articleService.GetByCategoryId(categoryId, request.Limit, request.Page);
            var mapArticles = AutoMapper.Mapper.Map<List<ArticlesView>>(articles);

            return Ok(mapArticles);
        }

        [HttpPost("rating/{id}")]
        public async Task<IActionResult> Rating(ArticleReitingRequest request, int id)
        {
            if (await _articleService.RatingArticle(id, request.Rating))
                return Ok();
            return BadRequest();
        }


        [HttpPost("saving/{id}")]
        public async Task<IActionResult> Saving(int id)
        {
            var userId = User.Claims.GetUserId();
            if (!userId.HasValue) return BadRequest();
            if (await _articleService.SaveArticle(userId.Value, id))
                return Ok();
            return BadRequest();

        }

        [HttpGet("saved")]
        public async Task<IActionResult> Saved([FromQuery]Paginating request)
        {
            var userId = User.Claims.GetUserId();
            if (!userId.HasValue) return BadRequest();
            var articles = await _articleService.GetSavedArticles(userId.Value, request.Limit, request.Page);
            var mapArticles = AutoMapper.Mapper.Map<List<SavedArticleView>>(articles);

            return Ok(mapArticles);

        }
        [AllowAnonymous]
        [HttpGet("author/{id}")]
        public async Task<IActionResult> AuthorArticles([FromQuery]Paginating request, int id)
        {
            var articles = await _articleService.ArticlesByThisAuthor(id, request.Limit, request.Page);
            var mapArticle = AutoMapper.Mapper.Map<List<ArticlesView>>(articles);
            return Ok(mapArticle);
        }
    }
}
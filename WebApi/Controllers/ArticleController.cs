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

        IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {

            this.articleService = articleService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await articleService.GetByIDAsync(id);
            if (article == null) return BadRequest();

            var viewArticle = AutoMapper.Mapper.Map<ArticleView>(article);

            return Ok(viewArticle);
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> List([FromQuery]Paginating request)
        {
            var articles = await articleService.GetList(request.Limit, request.Page);
            var _mapArticles = AutoMapper.Mapper.Map<List<ArticlesView>>(articles);

            return Ok(_mapArticles);
        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = User.Claims.GetUserId();
            if (user_id == null) return BadRequest();
            if (!await articleService.DeleteByIDAsync(id, user_id.Value)) return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleRequestcs request)
        {
            var maparticle = AutoMapper.Mapper.Map<Article>(request);
            var user_id = User.Claims.GetUserId();
            if (user_id == null) return BadRequest();

            if (!await articleService.CreateAsync(maparticle, user_id.Value))
                return NotFound();

            return Ok();

        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, PatchArticleRequest request)
        {
            var maparticle = AutoMapper.Mapper.Map<Article>(request);
            var user_id = User.Claims.GetUserId();
            if (user_id == null) return BadRequest();
            if (!await articleService.PatchAsync(id, maparticle, user_id.Value))
                return BadRequest();
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("category/{category_id}")]
        public async Task<IActionResult> GetByCategory(int category_id, [FromQuery]Paginating request)
        {

            var articles = await articleService.GetByCategoryID(category_id, request.Limit, request.Page);
            var _mapArticles = AutoMapper.Mapper.Map<List<ArticlesView>>(articles);

            return Ok(_mapArticles);
        }

        [HttpPost("rating/{id}")]
        public async Task<IActionResult> Rating(ArticleReitingRequest request, int id)
        {
            if (await articleService.RatingArticle(id, request.Rating))
                return Ok();
            return BadRequest();
        }


        [HttpPost("saving/{id}")]
        public async Task<IActionResult> Saving(int id)
        {
            int? userId = User.Claims.GetUserId();
            if (!userId.HasValue) return BadRequest();
            if (await articleService.SaveArticle(userId.Value, id))
                return Ok();
            return BadRequest();

        }

        [HttpGet("saved")]
        public async Task<IActionResult> Saved([FromQuery]Paginating request)
        {

            var user_id = User.Claims.GetUserId();
            if (!user_id.HasValue) return BadRequest();
            var articles = articleService.GetSavedArticles(user_id.Value, request.Limit, request.Page);
            var _mapArticles = AutoMapper.Mapper.Map<List<SavedArticleView>>(articles);

            return Ok(_mapArticles);

        }
        [AllowAnonymous]
        [HttpGet("author/{id}")]
        public async Task<IActionResult> AuthorArticles([FromQuery]Paginating request, int id)
        {
            var articles = await articleService.ArticlesByThisAuthor(id, request.Limit, request.Page);
            var _mapArticle = AutoMapper.Mapper.Map<List<ArticlesView>>(articles);
            return Ok(_mapArticle);

        }





    }
}
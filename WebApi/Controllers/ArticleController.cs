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
            if (article != null)
                return Ok(article);
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult List([FromQuery]Paginating request)
        {
            string orderBy = HttpContext.Request.Query["order_by"];
            var article = articleService.GetList(orderBy);

            return Ok(article.Skip(request.Limit * request.Page).Take(request.Limit));
        }

        [Authorize(Roles = "Writer, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (!await articleService.DeleteByIDAsync(id))
                return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "Writer, Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleRequestcs request)
        {
            var maparticle = AutoMapper.Mapper.Map<Article>(request);
            int? user_id = GetUserId();
            if (user_id == null) return BadRequest();

            if (!await articleService.CreateAsync(maparticle, user_id.Value))
                return NotFound();

            return Ok();

        }

        [Authorize(Roles = "Writer, Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, PatchArticleRequest request)
        {
            var maparticle = AutoMapper.Mapper.Map<Article>(request);
            if (!await articleService.PatchAsync(id, maparticle))
                return BadRequest();
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("category/{category_id}")]
        public IActionResult GetByCategory(int category_id, [FromQuery]Paginating request)
        {

            string orderBy = HttpContext.Request.Query["order_by"];
            var articles = articleService.GetByCategoryID(category_id, orderBy);

            return Ok(articles.Skip(request.Limit * request.Page).Take(request.Limit));
        }

        [HttpPost("rating/{id}")]
        public async Task<IActionResult> Rating(ArticleReitingRequest request, int id)
        {
            if (await articleService.RatingArticle(id, request.Rating))
                return Ok();
            return BadRequest();
        }


        public int? GetUserId()
        {

            var userId = User.Claims.FirstOrDefault(x => x.Type == "ID")?.Value;
            if (userId == null)
            {
                return null;
            }

            return Convert.ToInt32(userId);
        }

        [HttpPost("saving/{id}")]
        public async Task<IActionResult> Saving(int id)
        {
            int? userId = GetUserId();
            if (!userId.HasValue) return BadRequest();
            if (await articleService.SaveArticle(userId.Value, id))
                return Ok();
            return BadRequest();

        }

        [HttpGet("saved")]
        public async Task<IActionResult> Saved()
        {
            int? userId = GetUserId();
            if (!userId.HasValue) return BadRequest();
            var articles = articleService.GetSavedArticles(userId.Value);
            return Ok(articles);

        }



    }
}
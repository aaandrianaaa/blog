using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using WebApi.Requests;
using Service.Extensions;
using Service.Models;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("v1/api/comments")]
    [ApiController]
    [Authorize]

    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
      
        public CommentsController(ICommentService _commentService)
        {
            this._commentService = _commentService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Create(CreateCommentRequest request, int id)
        {
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();
            if (await _commentService.Create(id, userId.Value, request.Text)) return Ok();
            return BadRequest();
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentsByArticleId(int id, [FromQuery]Paginating request)
        {
            var comments = await _commentService.GetByArticleIdAsync(id, request.Limit, request.Page);
            var mapComment = AutoMapper.Mapper.Map<List<CommentsView>>(comments);
            return Ok(mapComment);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCommentAsync(int id, PatchCommentRequest request)
        {
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();
            if (!await _commentService.UpdateCommentAsync(id, request.Text, userId.Value)) return BadRequest();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();
            if (!await _commentService.DeleteByIdAsync(id, userId.Value)) return BadRequest();
            return Ok();
        }

        [HttpPost("like/{id}")]
        public async Task<IActionResult> LikeAndDislikeAsync(int id)
        {
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();
            if (!await _commentService.LikeAndDisLikeAsync(id, userId.Value)) return BadRequest();
            return Ok();
        }

        [HttpGet("like/{id}/users")]
        public async Task<IActionResult> GetUsersLikesListAsync(int id)
        {
            var users = await _commentService.ListUserLikesAsync(id);
            return Ok(users);
        }


    }
}
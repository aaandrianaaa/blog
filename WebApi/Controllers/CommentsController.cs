using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using WebApi.Requests;

namespace WebApi.Controllers
{
    [Route("v1/api/comments")]
    [ApiController]
    [Authorize]

    public class CommentsController : ControllerBase
    {
        ICommentService commentService;
        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
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

        [HttpPost("{id}")]
        public async Task<IActionResult> Create (CreateCommentRequest request, int id)
        {
            var user_id = GetUserId();
            if (user_id == null) return BadRequest();
            if (await commentService.Create(id, user_id.Value, request.Text)) return Ok();
            return BadRequest();
        }

    }
}
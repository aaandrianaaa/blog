using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Models;
using WebApi.Requests;
using WebApi.ViewModel;
using Service.Extensions;

namespace WebApi.Controllers
{

    [Route("v1/api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticateService _authService;
        private readonly IUserManagementService _userService;

        public UserController(IAuthenticateService authService, IUserManagementService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mapUser = AutoMapper.Mapper.Map<User>(request);
            if (await _userService.CreateAsync(mapUser))
            {
                return Ok();
            }

            return BadRequest();
        }
    
        [AllowAnonymous]
        [HttpPost("confirm/resend")]
        public async Task<IActionResult> ResendConfirmationMail(EmailRequest request)
        {
            if (await _userService.SendConfirmation(request.Mail))
                return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmMail(CreateConfirmRequest request)
        {
            var mapConfirm = AutoMapper.Mapper.Map<Confirmation>(request);
            var result = await _userService.CofirmMail(mapConfirm);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost("{id}/role")]
        public async Task<IActionResult> ChangeRole(int id, NewRoleRequest request)
        {
            var userId = User.Claims.GetUserId();
            if (userId == null) return BadRequest();

            if (await _userService.ChangeRole(id, request.Role, userId.Value))
                return Ok();
            return BadRequest();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = User.Claims.GetUserId();

            if (userId == null) return BadRequest();
            if (await _userService.DeleteUser(userId.Value)) return Ok();

            return BadRequest();
        }


        [Authorize]
        [HttpPatch("")]
        public async Task<IActionResult> PatchUser(PatchUserRequest request)
        {
            var userId = User.Claims.GetUserId();

            if (userId == null) return BadRequest();

            var mapUser = AutoMapper.Mapper.Map<User>(request);

            if (await _userService.PatchUser(userId.Value, mapUser)) return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var user = await _userService.GetUserByIDAsync(id);
            if (user == null) return BadRequest();
            var mapUser = AutoMapper.Mapper.Map<ViewModel.UserView>(user);
            return Ok(mapUser);
        }

        [Authorize(Roles ="Moderator, Admin")]
        [HttpPost("{id}/block")]
        public async Task<IActionResult> BlockUser (int id)

        {
            if (!await _userService.BlockUserAsync(id)) return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost("{id}/unblock")]
        public async Task<IActionResult> UnBlockUser(int id)

        {
            if (!await _userService.UnBlockUser(id)) return BadRequest();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> UsersList([FromQuery]Paginating request)
        {
            
            var users = await _userService.UsersList(request.Limit, request.Page);
            var mapUsers = AutoMapper.Mapper.Map<List<UsersView>>(users);
            return Ok(mapUsers);
        }

    }
}
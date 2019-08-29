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

            var mapuser = AutoMapper.Mapper.Map<User>(request);
            if (await _userService.CreateAsync(mapuser))
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
            var mapconfirm = AutoMapper.Mapper.Map<Confirmation>(request);
            var result = await _userService.CofirmMail(mapconfirm);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost("change/role/{id}")]
        public async Task<IActionResult> ChangeRole(int id, NewRoleRequest request)
        {
            var user_id = User.Claims.GetUserId();
            if (user_id == null) return BadRequest();

            if (await _userService.ChangeRole(id, request.Role, user_id.Value))
                return Ok();
            return BadRequest();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var user_id = User.Claims.GetUserId();

            if (user_id == null) return BadRequest();
            if (await _userService.DeleteUser(user_id.Value)) return Ok();

            return BadRequest();
        }


        [Authorize]
        [HttpPost("pathc")]
        public async Task<IActionResult> PatchUser(PatchUserRequest request)
        {
            var user_id = User.Claims.GetUserId();

            if (user_id == null) return BadRequest();

            var _mapUser = AutoMapper.Mapper.Map<User>(request);

            if (await _userService.PatchUser(user_id.Value, _mapUser)) return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var user = await _userService.GetUserByIDAsync(id);
            if (user == null) return BadRequest();
            var _mapUser = AutoMapper.Mapper.Map<ViewModel.UserView>(user);
            return Ok(_mapUser);
        }

        [Authorize(Roles ="Moderator, Admin")]
        [HttpPost("block/{id}")]
        public async Task<IActionResult> BlockUser (int id)

        {
            if (!await _userService.BlockUserAsync(id)) return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost("unblock/{id}")]
        public async Task<IActionResult> UnBlockUser(int id)

        {
            if (!await _userService.UnBlockUser(id)) return BadRequest();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> UsersList([FromQuery]Paginating request)
        {
            
            var users = await _userService.UsersList(request.Limit, request.Page);
            var _mapUsers = AutoMapper.Mapper.Map<List<UsersView>>(users);
            return Ok(_mapUsers);
        }

    }
}
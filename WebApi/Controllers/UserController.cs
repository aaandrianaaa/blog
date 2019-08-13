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
            if (!await _userService.CreateAsync(mapuser))
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(EmailRequest request)
        {
            if (await _userService.SendConfirmation(request.Mail))
                return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("cofirm")]
        public async Task<IActionResult> Confirmt(CreateConfirmRequest request)
        {
            var mapconfirm = AutoMapper.Mapper.Map<Confirmation>(request);
            var result = await _userService.CofirmMail(mapconfirm);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize(Roles = "Writer")]
        [HttpGet]
        public async Task<IActionResult> Role()
        {
            var role = User.Claims.ToList();
            return Ok($"{User.Identity.Name}");

        }

    }
}
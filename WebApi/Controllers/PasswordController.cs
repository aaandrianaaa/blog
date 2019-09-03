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

namespace WebApi.Controllers
{
    [Route("v1/api/password")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IUserManagementService _userService;
        public PasswordController(IAuthenticateService authService, IUserManagementService userService)
        {
          
            _userService = userService;
        }

      
        [AllowAnonymous]
        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword(EmailRequest request)
        {
            if (await _userService.SendConfirmationPassword(request.Mail))
                return Ok();
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPassword(CreateConfirmRequest request)
        {
            if (await _userService.ForgotPassword(request.Mail, request.Number))
                return Ok();
            return BadRequest();
        }

        [Authorize]
        [HttpPost("change")]
        public async Task<IActionResult> ChangePassword(NewPasswordRequest request)
        {
            var userId = User.Claims.GetUserId();
            if (!userId.HasValue) return BadRequest();
            if (await _userService.ChangePassword(userId.Value, request.OldPassword, request.NewPassword))
                return Ok();
            return BadRequest();
        }
            
        [AllowAnonymous]
        [HttpPost("resend")]
        public async Task<IActionResult> ResendConfirmPassword(EmailRequest request)
        {
            if (await _userService.ResendConfirmPasswors(request.Mail)) return Ok();
            return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Requests;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("v1/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthenticateService _authService;
        private readonly IUserManagementService _userService;

        public AuthController(IAuthenticateService authService, IUserManagementService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Service.Models.TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var token = await _authService.IsAuthenticated(request);

            if (token != null)
            {
                return Ok(new LoginView()
                {
                    Token = token
                });
            }

            return BadRequest();
        }
    }
}

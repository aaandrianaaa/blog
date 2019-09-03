using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Requests;
using WebApi.ViewModel;
//using static System.Collections.Specialized.BitVector32;

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
        public IActionResult Login(Service.Models.TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_authService.IsAuthenticated(request, out var token))
            {
                return Ok(new LoginView() {
                    Token = token
                });
            }

            return BadRequest();
        }


    

    
        
      

       
    }
}

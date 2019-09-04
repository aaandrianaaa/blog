﻿using Microsoft.Extensions.Options;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Service.Implementations
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
        }
        public bool IsAuthenticated(TokenRequest request, out string token)
        {

            token = string.Empty;

            var user = _userManagementService.GetUser(request.Email, request.Password);
            if (user.Blocked == true && user.BlockedUntil < DateTime.Now) user.Blocked = false;
            if (user == null || user.DeletedAt != null || user.Blocked || !user.Activated) return false;
          
            var claim = new[]
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("ID", user.ID.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;

        }
    }
}

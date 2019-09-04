using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.Extensions
{
    public static class AuthExtension
    {
        public static IEnumerable<Claim> GetUserClaims(this User user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("ID", user.ID.ToString())
            };
        }

        public static int? GetUserId(this IEnumerable<Claim> claims)
        {
            var userId = claims.FirstOrDefault(x => x.Type == "ID")?.Value;
            if (userId == null)
            {
                return null;
            }

            return Convert.ToInt32(userId);
        }

     
    }
}

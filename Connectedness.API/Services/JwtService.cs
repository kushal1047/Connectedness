using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Connectedness.API.Models;
using System.Security.Cryptography;

namespace Connectedness.API.Services
{
    public class JwtService(IConfiguration config)
    {
        private readonly IConfiguration _config = config;
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString() ),
            new Claim(ClaimTypes.Name, user.FullName ),
            new Claim(ClaimTypes.Email, user.Email )
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken( token );
        }
    }
}
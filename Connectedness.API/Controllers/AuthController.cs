using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Connectedness.API.Models;
using Connectedness.API.Services;
using Connectedness.API.Data;

namespace Connectedness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(JwtService jwtService, AppDbContext context): ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly JwtService _jwtService = jwtService;

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var tokenEntity = await _context.RefreshTokens.Include(token => token.User).FirstOrDefaultAsync(token=> token.Token == refreshToken);
            if (tokenEntity == null || !tokenEntity.isActive) {
                return Unauthorized("Invalid or expired token!");
            }

            tokenEntity.Revoked = DateTime.UtcNow;

            var newJwtToken = _jwtService.GenerateToken(tokenEntity.User!);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            var newRefreshTokenEntity = new RefreshToken()
            {
                Token = newRefreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(30),
                UserId = tokenEntity.UserId,
            };
            _context.RefreshTokens.Add(newRefreshTokenEntity);
            await _context.SaveChangesAsync();
            return Ok(new {token=newJwtToken, refreshToken=newRefreshToken, userId = tokenEntity.UserId});
        }
    }
}
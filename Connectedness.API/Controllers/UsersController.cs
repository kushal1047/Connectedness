using Connectedness.API.Data;
using Connectedness.API.Models;
using Connectedness.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Connectedness.API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Connectedness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(AppDbContext context, JwtService jwtService): ControllerBase {
        private readonly AppDbContext _context = context;
        private readonly JwtService _jwtService = jwtService;
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) {
                return BadRequest(new { message = "Invalid Email or Password. Please try again." });
            }
            var jwtToken = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var refreshTokenEntity = new RefreshToken()
            {
                Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(30),
                UserId = user.UserId,
            };
            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Login successful!", userId = user.UserId, token = jwtToken, refreshToken});
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            // checks whether the user email is previously registered.
            var existingUser = await _context.Users.FirstOrDefaultAsync(u=> u.Email == dto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email is already registered!" });
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var newUser = new User() { 
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            Gender = dto.Gender,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok(new { message= "User registeration successful." });
        }

        [Authorize]
        [HttpGet("groups")]
        public IActionResult GetGroupsForUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var groups = _context.GroupMembers
                         .Where(gm => gm.UserId == userId)
                         .Select(gm => new
                         {
                             gm.Group!.GroupId,
                             gm.Group.GroupName,
                             gm.Group.CreatedAt,
                             CreatedBy = gm.Group.CreatedByUser!.FullName,
                             Members = gm.Group.GroupMembers.Select(member => new {
                                 member.User!.UserId,
                                 member.User.FullName,
                                 member.User.Email
                             }).ToList()
                         }).ToList();
            if (groups.Count == 0)
            {
                return NotFound(new {message= "User doesn't belong to any group."});
            }
            return Ok(groups);
        }
    }
}

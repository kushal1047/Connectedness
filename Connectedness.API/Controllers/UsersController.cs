using Connectedness.API.Data;
using Connectedness.API.Models;
using Connectedness.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Connectedness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) {
            _context = context; 
       }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) {
                return Unauthorized("Invalid Email or Password. Please try again.");
            }
            return Ok(new {message = "Login successful!", userId = user.UserId});
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto dto)
        {
            // checks whether the user email is previously registered.
            var existingUser = _context.Users.FirstOrDefault(u=> u.Email == dto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already registered");
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
            return Ok(new { message= "User registeration successful!", userId= newUser.UserId });
        }

        [HttpGet("{userId}/groups")]
        public IActionResult GetGroupsForUser(int userId)
        {
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
            if (!groups.Any())
            {
                return NotFound("User doesn't belong to any group.");
            }
            return Ok(groups);
        }
    }
}

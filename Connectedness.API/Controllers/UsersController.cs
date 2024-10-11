using Connectedness.API.Data;
using Connectedness.API.Models;
using Connectedness.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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
            if (user == null || user.PasswordHash != dto.Password) {
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
            var newUser = new User() { 
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = dto.Password, // Note: This should be hashed in production!
            Gender = dto.Gender,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok(new { message= "User registeration successful!", userId= newUser.UserId });
        }
    }
}

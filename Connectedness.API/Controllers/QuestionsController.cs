using Connectedness.API.Models;
using Connectedness.API.Data;
using Connectedness.API.DTOs;
using Connectedness.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Connectedness.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController(AppDbContext context): ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("create")]
        public IActionResult CreateQuestion(QuestionCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var group = _context.Groups
                .Include(group => group.GroupMembers)
                .FirstOrDefault(group => group.GroupId == dto.GroupId);
            if (group == null) { 
                return NotFound("Group not found!");
            }
            if (!(group.GroupMembers.Any(member=> member.UserId == userId)))
            {
                return StatusCode(403, new { message = "Only group members can post questions in this group." });
            }
            var question = new Question()
            {
                Text = dto.Text,
                CorrectAnswer = dto.CorrectAnswer,
                IncorrectOption1 = dto.IncorrectOption1,
                IncorrectOption2 = dto.IncorrectOption2,
                IncorrectOption3 = dto.IncorrectOption3,
                GroupId = dto.GroupId,
                CreatedByUserId = userId,
            };
            _context.Questions.Add(question);
            _context.SaveChanges();
            return Ok(new {message="Question created successfully."});
        }

    }
}
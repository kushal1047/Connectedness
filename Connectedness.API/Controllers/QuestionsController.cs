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

        [HttpGet("group/{groupId}")]
        public IActionResult GetGroupQuestions(int groupId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var isGroupMember = _context.GroupMembers.Any(member => member.GroupId == groupId && member.UserId == userId);
            if (!isGroupMember)
            {
                return StatusCode(403, new { message = "User is not a member of this group." });
            }
            var questions = _context.Questions
                            .Include(question=>question.CreatedByUser)
                            .Where(question => question.GroupId == groupId)
                            .Select(question => new
                            {
                                question.QuestionId,
                                question.Text,
                                createdBy = new { question.CreatedByUserId, question.CreatedByUser!.FullName },
                                options = new List<string>
                                {
                                    question.CorrectAnswer,
                                    question.IncorrectOption1,
                                    question.IncorrectOption2,
                                    question.IncorrectOption3
                                }
                            }).ToList();
            return Ok(questions);
        }

    }
}
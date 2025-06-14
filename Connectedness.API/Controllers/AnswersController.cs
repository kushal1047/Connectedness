using Connectedness.API.Models;
using Connectedness.API.DTOs;
using Connectedness.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Connectedness.API.Controllers
{   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("submit")]
        public IActionResult SubmitAnswer([FromQuery] int questionId, [FromBody] string answerText) {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var question = _context.Questions.FirstOrDefault(question => question.QuestionId == questionId);
            if (question == null) {
                return NotFound("Question doesn't exist.");
            }
            var isMember = _context.GroupMembers.Any(member=> member.GroupId == question.GroupId && member.UserId == userId);
            if (!isMember) {
                return StatusCode(403, "User is not a member of this group.");
            }
            if (question.CreatedByUserId == userId)
            {
                return BadRequest("User cannot answer own questions.");
            }
            var isAnswered = _context.Answers.Any(answer=> answer.QuestionId == questionId && answer.AnsweredByUserId == userId);
            if (isAnswered)
            {
                return BadRequest("User has already answered this question.");
            }
            var answer = new Answer
            {
                QuestionId = questionId,
                SelectedAnswer = answerText,
                IsCorrect = question.CorrectAnswer == answerText,
                AnsweredByUserId = userId
            };
            _context.Answers.Add(answer);
            _context.SaveChanges();
            return Ok(new {message= "Answer submitted successfully", isCorrect = answer.IsCorrect });
        }

        [HttpGet("{groupId}/user-result")]
        public IActionResult GetUserResult(int groupId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var isMember = _context.GroupMembers.Any(member=>member.GroupId == groupId && member.UserId == userId);
            if (!isMember)
            {
                return StatusCode(403,"User is not a member of this group.");
            }
            var questionIds = _context.Questions.Where(question=> question.GroupId == groupId).Select(question=>question.QuestionId).ToList();
            var userAnswers = _context.Answers.Include(answer=> answer.Question).Where(answer => questionIds.Contains(answer.QuestionId) && answer.AnsweredByUserId == userId).ToList();
            var totalAnswered = userAnswers.Count;
            var correctAnswers = userAnswers.Count(answer=>answer.IsCorrect);
            var userScore = totalAnswered > 0 ? $"{(correctAnswers * 100 / totalAnswered)}%" : $"0%";
            var userResult = new { 
               totalAnswered,
               correctAnswers,
               userScore,
               answersList = userAnswers.Select(answer=> new
               {
                   answer.QuestionId,
                   answer.Question!.Text,
                   answer.SelectedAnswer,
                   answer.IsCorrect,
                   answer.Question.CorrectAnswer
               }).ToList()
            };
            return Ok(userResult);

        }
        [HttpGet("{groupId}/group-result")]
        public async Task<IActionResult> GetGroupResults(int groupId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var isMember = await _context.GroupMembers.AnyAsync(member=>member.GroupId == groupId && member.UserId == userId);
            if (!isMember)
            {
                return StatusCode(403, "Only group members can view the result");
            }
            var memberIds = await _context.GroupMembers.Where(member => member.GroupId == groupId).Select(member=> member.UserId).ToListAsync();
            var questionIds = await _context.Questions.Where(question=>question.GroupId == groupId).Select(question=>question.QuestionId).ToListAsync();
            var answers = await _context.Answers.Include(answer=>answer.Question).Where(answer => questionIds.Contains(answer.QuestionId) && memberIds.Contains(answer.AnsweredByUserId)).ToListAsync();
            var validAnswers = answers.Where(answer=>answer.AnsweredByUserId != answer.Question!.CreatedByUserId).ToList();
            var totalAnswered = validAnswers.Count;
            var correctAnswers = validAnswers.Count(answer=>answer.IsCorrect);
            var totalExpectedAnswers = questionIds.Count * (memberIds.Count - 1);
            var groupScore = totalAnswered > 0 ? $"{correctAnswers * 100 / totalExpectedAnswers}%" : "0%";
            var groupResult = new {
                totalAnswered,
                correctAnswers,
                totalExpectedAnswers,
                groupScore,
                memberAnswers = validAnswers.Select(answer => new
                {
                    answer.QuestionId,
                    answer.Question!.Text,
                    answer.SelectedAnswer,
                    answer.IsCorrect,
                    answer.Question.CorrectAnswer
                }).ToList()
            };
            return Ok(groupResult);
        }
    }
}


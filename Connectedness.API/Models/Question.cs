namespace Connectedness.API.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public required string Text { get; set; } 
        public int CreatedByUserId { get; set; }
        public required User CreatedByUser { get; set; }
        public required string CorrectAnswer { get; set; }
        public required string[] Options { get; set; }
    }
}
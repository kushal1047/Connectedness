namespace Connectedness.API.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public required string Text { get; set; } 
        public int CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
        public required string CorrectAnswer { get; set; }
        public required string IncorrectOption1 { get; set; }
        public required string IncorrectOption2 { get; set; }
        public required string IncorrectOption3 { get; set; }
        public int GroupId { get; set; }
        public Group? Group { get; set; }

    }
}
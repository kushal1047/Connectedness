namespace Connectedness.API.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; } 
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] Options { get; set; }
    }
}
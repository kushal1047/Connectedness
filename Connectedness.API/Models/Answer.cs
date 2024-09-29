namespace Connectedness.API.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string SelectedAnswer { get; set; }
        public bool isCorrect { get; set; }
        public int AnsweredByUserId { get; set; }
        public User AnsweredByUser { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
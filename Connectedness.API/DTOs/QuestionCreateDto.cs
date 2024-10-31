namespace Connectedness.API.DTOs
{
    public class QuestionCreateDto
    {
        public required string Text { get; set; }
        public required string CorrectAnswer { get; set; }
        public required string IncorrectOption1 { get; set; }
        public required string IncorrectOption2 { get; set; }
        public required string IncorrectOption3 { get; set; }
        public required string GroupId { get; set; }
    }
}
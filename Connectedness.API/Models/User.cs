namespace Connectedness.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Gender { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
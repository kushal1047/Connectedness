namespace Connectedness.API.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool isActive => Revoked == null && !IsExpired; 

        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
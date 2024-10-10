namespace Connectedness.API.DTOs
{
    public class GroupCreateDto
    {
        public string GroupName { get; set; }
        public int CreatorUserId { get; set; } 
        public List<int> MemberUserIds { get; set; }
    }
}
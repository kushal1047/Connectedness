namespace Connectedness.API.DTOs
{
    public class GroupCreateDto
    {
        public required string GroupName { get; set; }
        public required List<int> MemberUserIds { get; set; }
    }
}
namespace Connectedness.API.Models;

public class Group
{
    public int GroupId { get; set; }
    public required string GroupName { get; set; }
    public DateTime CreatedAt { get; set; }
    public required ICollection<GroupMember> GroupMembers { get; set; }
}

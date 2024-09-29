namespace Connectedness.API.Models;

public class Group
{
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<GroupMember> GroupMembers { get; set; }
}

using Connectedness.API.Data;
using Connectedness.API.Models;
using Connectedness.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Connectedness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("create")]
        public IActionResult CreateGroup (GroupCreateDto dto)
        {
            var group = new Group()
            {
                GroupName = dto.GroupName,
                CreatedAt = DateTime.UtcNow,
                GroupMembers = new List<GroupMember>()
            };
            var uniqueMemberIds = dto.MemberUserIds.Prepend(dto.CreatorUserId).ToList().Distinct();
            foreach (var memberId in uniqueMemberIds)
            {
                group.GroupMembers.Add(new GroupMember { UserId = memberId});
            }
            _context.Groups.Add(group);
            _context.SaveChanges();
            return Ok(new {message = "Group Created Successfully!", groupId = group.GroupId });
        }

        
    }
}
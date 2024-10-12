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
                CreatedByUserId = dto.CreatorUserId,
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

        [HttpGet("{id}")]
        public IActionResult GetGroup(int id)
        {
            var group = _context.Groups.Where(group => group.GroupId == id)
                        .Select(group => new { 
                            group.GroupId,
                            group.GroupName,
                            group.CreatedAt,
                            CreatedBy = group.CreatedByUser!.FullName,
                            Members = group.GroupMembers.Select(member=> new
                            {
                                member.UserId,
                                member.User!.FullName,
                                member.User.Email
                            }).ToList()
                        }).FirstOrDefault();
            if (group == null)
            {
                return NotFound("Group not found!");
            }
            return Ok(group);
        }

        [HttpGet("user/{userId}")]

        public IActionResult GetGroupsForUser(int userId)
        {
            var groups = _context.GroupMembers
                         .Where(gm=> gm.UserId == userId)
                         .Select(gm=> new
                         {
                             gm.Group!.GroupId,
                             gm.Group.GroupName,
                             gm.Group.CreatedAt,
                             CreatedBy = gm.Group.CreatedByUser!.FullName,
                             Members= gm.Group.GroupMembers.Select(member=> new { 
                             member.User!.UserId,
                             member.User.FullName,
                             member.User.Email
                             }).ToList()
                         }).ToList();
            if (!groups.Any())
            {
                return NotFound("User doesn't belong to any group.");
            }
            return Ok(groups);
        }

        
    }
}
using Connectedness.API.Data;
using Connectedness.API.Models;
using Connectedness.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Connectedness.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("create")]
        public IActionResult CreateGroup (GroupCreateDto dto)
        {
            var creatorUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var group = new Group()
            {
                GroupName = dto.GroupName,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = creatorUserId,
                GroupMembers = []
            };
            var uniqueMemberIds = dto.MemberUserIds.Prepend(creatorUserId).ToList().Distinct();
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

        [HttpPut("{groupId}/update-name")]
        public IActionResult UpdateGroupName(int groupId, [FromBody] string newName, [FromQuery] int userId)
        {   if (string.IsNullOrWhiteSpace(newName))
            {
                return BadRequest("Group name cannot be empty.");
            }
            var group = _context.Groups.Where(group => group.GroupId == groupId).FirstOrDefault();
            if (group == null) {
                return NotFound("Group not found!");
            }
            if (userId != group.CreatedByUserId)
            {
                return Unauthorized("Only user admin can update the group name.");
            }
            group.GroupName = newName;
            _context.SaveChanges();
            return Ok(new {message = "Group name updated successfully.", group});

        }
        [HttpDelete("{groupId}/leave/{userId}")]
        public IActionResult LeaveGroup(int groupId, int userId)
        {
            var group = _context.Groups.
                Include(group=>group.GroupMembers).
                Where(group => group.GroupId == groupId).FirstOrDefault();
            if (group == null)
            {
                return NotFound("Unable to find group.");
            }
            var user = group.GroupMembers.Where(member=> member.UserId == userId).FirstOrDefault();
            if (user == null)
            {
                return NotFound("User is not a member of this group.");
            }
            if (user.UserId == group.CreatedByUserId)
            {
                return BadRequest("Group Creators cannot leave the group. They may only transfer the ownership or delete the group.");
            }
            _context.GroupMembers.Remove(user);
            _context.SaveChanges();
            return Ok("The user has successfully left the group.");
        }

        [HttpDelete("{groupId}")]
        public IActionResult DeleteGroup(int groupId, [FromQuery] int userId)
        {
            var group = _context.Groups
                .Include(group=>group.GroupMembers)
                .FirstOrDefault(group=> group.GroupId == groupId);
            if (group == null)
            {
                return NotFound("Group not found.");
            }
            if (userId != group.CreatedByUserId)
            {
                return StatusCode(403, "Only group admin can delete this group.");
            }
            _context.GroupMembers.RemoveRange(group.GroupMembers);
            _context.Groups.Remove(group);
            _context.SaveChanges();
            return Ok(new {message = "Group deleted successfully."});

        }

        [HttpPut("{groupId}/transfer-ownership")]
        public IActionResult TransferOwnership(int groupId, [FromQuery] int userId, [FromQuery] int newOwnerId)
        {
            var group = _context.Groups
                        .Include(group => group.GroupMembers)
                        .Include(group => group.CreatedByUser)
                        .FirstOrDefault(group => group.GroupId == groupId);
            if (group == null)
            {
                return NotFound("Group Not Found!");
            }
            if (!(group.GroupMembers.Any(member=>member.UserId == newOwnerId)))
            {
                return BadRequest("User can transfer ownership to a group member only.");
            }
            if (group.CreatedByUserId != userId)
            {
                return Unauthorized("Only group admin can transfer ownership.");
            }
            group.CreatedByUserId = newOwnerId;
            _context.SaveChanges();
            _context.Entry(group).Reference(group => group.CreatedByUser).Load();
            return Ok(new { message= $"Group ownership is transfered successfully. New group owner is {group.CreatedByUser!.FullName}"});
        }
    }
}
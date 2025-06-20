using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using CareerGuidancePlatform.Models;
using CareerGuidancePlatform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]   
    public class MentorshipController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MentorshipController(AppDbContext context)
        {
            _context = context;
        }

        // Helper để parse userId từ claim
        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("User ID claim not found.");
            return int.Parse(claim.Value);
        }

        [HttpPost("add-mentor")]
        public async Task<IActionResult> AddMentor([FromBody] Mentor mentor)
        {
            _context.Mentors.Add(mentor);
            await _context.SaveChangesAsync();
            return Ok("Mentor added");
        }


        [HttpGet("mentors")]
        public async Task<IActionResult> GetMentors(
            [FromQuery] string? field,
            [FromQuery] string? specialization,
            [FromQuery] string? availability)
        {
            // 1. Bắt đầu từ Mentors, Include User
            var query = _context.Mentors
                                .Include(m => m.User)
                                .AsQueryable();

            // 2. Áp filter nếu có
            if (!string.IsNullOrWhiteSpace(field))
                query = query.Where(m => m.Career.Contains(field));
            if (!string.IsNullOrWhiteSpace(specialization))
                query = query.Where(m => m.Niche.Contains(specialization));
            if (!string.IsNullOrWhiteSpace(availability))
                query = query.Where(m => m.Availability.Contains(availability));

            // 3. Project về DTO
            var result = await query
                .Select(m => new MentorWithUserDto
                {
                    MentorId     = m.MentorId,
                    Career       = m.Career,
                    Niche        = m.Niche,
                    Availability = m.Availability,
                    // …
                    UserId       = m.UserId,
                    Username     = m.User!.Username,
                    FullName     = m.User.FullName,
                    Role         = m.User!.Role,
                    Email        = m.User!.Email
                    // … các trường User khác
                })
                .ToListAsync();

            return Ok(result);
        }


        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            var message = new Message
            {
                SenderId   = GetUserId(),           // lấy từ claim
                ReceiverId = dto.ReceiverId,        // từ body
                Content    = dto.Content,           // từ body
                Timestamp  = DateTime.UtcNow        // set hiện tại
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok("Message sent");
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages(int otherUserId)
        {
            var userId = GetUserId();
            var messages = await _context.Messages
                .Where(m =>
                    (m.SenderId == userId && m.ReceiverId == otherUserId) ||
                    (m.SenderId == otherUserId && m.ReceiverId == userId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpGet("conversations")]
        public async Task<IActionResult> GetConversations([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            // 1. Lấy userId từ token
            var userId = GetUserId();;

            // 2. Truy vấn và group message
            var rawConvs = await _context.Messages
                .Where(m => m.SenderId  == userId || m.ReceiverId == userId)
                .Select(m => new {
                    PartnerId    = m.SenderId  == userId ? m.ReceiverId : m.SenderId,
                    m.Content,
                    m.Timestamp,
                    m.ReceiverId,
                })
                .GroupBy(x => x.PartnerId)
                .Select(g => new {
                    PartnerId     = g.Key,
                    LastTimestamp = g.Max(x => x.Timestamp),
                    LastMessage   = g
                        .OrderByDescending(x => x.Timestamp)
                        .Select(x => x.Content)
                        .FirstOrDefault(),
                    UnreadCount   = g.Count(x => x.ReceiverId == userId)
                })
                .OrderByDescending(x => x.LastTimestamp)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            // 3. Lấy profile của tất cả partner trong một query
            var partnerIds = rawConvs.Select(c => c.PartnerId).ToList();
            var profiles = await _context.Users
                .Where(u => partnerIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u);

            // 4. Ghép lại thành DTO
            var result = rawConvs.Select(c => new ConversationDto
            {
                PartnerId = c.PartnerId,
                PartnerName = profiles[c.PartnerId].FullName,
                PartnerUsername = profiles[c.PartnerId].Username,
                ParnerRole = profiles[c.PartnerId].Role,
                AvatarUrl = $"https://api.dicebear.com/9.x/pixel-art/svg?seed={Uri.EscapeDataString(profiles[c.PartnerId].Username)}",
                LastMessage = c.LastMessage,
                LastTimestamp = c.LastTimestamp,
            });

            return Ok(result);
        }



    }
}

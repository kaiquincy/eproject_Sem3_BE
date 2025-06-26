using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StatisticsController(AppDbContext context) => _context = context;

        // 1. Tổng quan hiện tại
        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview()
        {
            var totalUsers = await _context.Users.CountAsync();
            var pendingMentors = await _context.Mentors.CountAsync(m => m.Status == MentorStatus.Pending);
            var approvedMentors = await _context.Mentors.CountAsync(m => m.Status == MentorStatus.Approved);
            var totalCompanies = await _context.Companies.CountAsync();
            var totalJobs = await _context.Jobs.CountAsync();
            var totalMessages = await _context.Messages.CountAsync();
            return Ok(new
            {
                totalUsers,
                pendingMentors,
                approvedMentors,
                totalCompanies,
                totalJobs,
                totalMessages
            });
        }

        // 2. Số người đăng ký theo ngày (x ngày gần nhất)
        [HttpGet("daily-registrations")]
        public async Task<IActionResult> GetDailyRegistrations([FromQuery] int days = 30)
        {
            var today = DateTime.UtcNow.Date;
            var startDate = today.AddDays(-days + 1);

            // 1 truy vấn: lọc, group by theo date, count
            var grouped = await _context.Users
                .Where(u => u.CreatedAt >= startDate && u.CreatedAt < today.AddDays(1))
                .GroupBy(u => u.CreatedAt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // Ổn định lại: đảm bảo các ngày thiếu có count = 0
            var result = Enumerable.Range(0, days)
                .Select(i => {
                    var date = startDate.AddDays(i);
                    var entry = grouped.FirstOrDefault(g => g.Date == date);
                    return new { date, count = entry?.Count ?? 0 };
                });

            return Ok(result);
        }

        // 3. Số mentor request theo ngày
        [HttpGet("daily-mentor-requests")]
        public async Task<IActionResult> GetDailyMentorRequests([FromQuery] int days = 30)
        {
            var today = DateTime.UtcNow.Date;
            var startDate = today.AddDays(-days + 1);

            // 1 truy vấn duy nhất: lọc theo khoảng, group by ngày, đếm số request
            var grouped = await _context.Mentors
                .Where(m => m.RequestedAt >= startDate && m.RequestedAt < today.AddDays(1))
                .GroupBy(m => m.RequestedAt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // Tạo mảng kết quả đảm bảo ngày nào không có request sẽ có count = 0
            var result = Enumerable.Range(0, days)
                .Select(i =>
                {
                    var date = startDate.AddDays(i);
                    var entry = grouped.FirstOrDefault(g => g.Date == date);
                    return new { date, count = entry?.Count ?? 0 };
                });

            return Ok(result.OrderBy(r => r.date));
        }


        // 4. Số job được đăng theo ngày
        [HttpGet("daily-jobs-posted")]
        public async Task<IActionResult> GetDailyJobsPosted([FromQuery] int days = 30)
        {
            var today = DateTime.UtcNow.Date;
            // Tính ngày bắt đầu (ngày cũ nhất cần lấy)
            var startDate = today.AddDays(-days + 1);

            // 1 query duy nhất: lọc theo khoảng, group by ngày, đếm số job
            var grouped = await _context.Jobs
                .Where(j => j.PostedDate >= startDate && j.PostedDate < today.AddDays(1))
                .GroupBy(j => j.PostedDate.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // “Đắp” các ngày thiếu về count = 0
            var result = Enumerable.Range(0, days)
                .Select(i =>
                {
                    var date = startDate.AddDays(i);
                    var entry = grouped.FirstOrDefault(g => g.Date == date);
                    return new { date, count = entry?.Count ?? 0 };
                })
                .OrderBy(r => r.date);

            return Ok(result);
        }


        // 5. Số tin nhắn gửi theo ngày
        [HttpGet("daily-messages-sent")]
        public async Task<IActionResult> GetDailyMessagesSent([FromQuery] int days = 30)
        {
            var today = DateTime.UtcNow.Date;
            var startDate = today.AddDays(-days + 1);

            // 1 query duy nhất: lọc theo khoảng, group by theo date, đếm số message
            var grouped = await _context.Messages
                .Where(m => m.Timestamp >= startDate && m.Timestamp < today.AddDays(1))
                .GroupBy(m => m.Timestamp.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // “Đắp” các ngày không có message để count = 0
            var result = Enumerable.Range(0, days)
                .Select(i =>
                {
                    var date = startDate.AddDays(i);
                    var entry = grouped.FirstOrDefault(g => g.Date == date);
                    return new { date, count = entry?.Count ?? 0 };
                })
                .OrderBy(r => r.date);

            return Ok(result);
        }

    }
}

using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Entities;

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Route("api/user/calendar")]
    [Authorize]  // Bắt buộc phải có JWT
    public class CalendarController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CalendarController(AppDbContext db)
        {
            _db = db;
        }

        // GET api/user/calendar
        [HttpGet]
        public async Task<IActionResult> GetCalendar()
        {
            // Lấy userId từ claim
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            // Tìm entry trong DB
            var entry = await _db.UserCalendars.FindAsync(userId);
            if (entry == null)
            {
                // Nếu chưa có, trả về object rỗng
                return Ok(new Dictionary<string, List<int>>());
            }

            // Deserialize JSON -> Dictionary<string, List<int>>
            var schedule = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(entry.ScheduleJson)
                           ?? new Dictionary<string, List<int>>();

            return Ok(schedule);
        }

        // POST api/user/calendar
        [HttpPost]
        public async Task<IActionResult> SaveCalendar([FromBody] Dictionary<string, List<int>> schedule)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            // Serialize lại thành JSON
            var json = JsonSerializer.Serialize(schedule);

            // Upsert record
            var entry = await _db.UserCalendars.FindAsync(userId);
            if (entry == null)
            {
                entry = new UserCalendar
                {
                    UserId = userId,
                    ScheduleJson = json
                };
                _db.UserCalendars.Add(entry);
            }
            else
            {
                entry.ScheduleJson = json;
                _db.UserCalendars.Update(entry);
            }

            await _db.SaveChangesAsync();
            return Ok();    // hoặc return NoContent();
        }
    }
}

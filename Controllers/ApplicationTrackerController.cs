using Microsoft.AspNetCore.Mvc;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

[ApiController]
[Route("api/[controller]")]
public class ApplicationTrackerController : ControllerBase
{
    private readonly AppDbContext _context;

    public ApplicationTrackerController(AppDbContext context) => _context = context;

    // GET: api/applicationtracker/user/1
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetApplications(int userId) =>
        Ok(await _context.ApplicationTrackers
            .Include(a => a.Job)
            .Where(a => a.UserId == userId)
            .ToListAsync());

    // POST: api/applicationtracker
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ApplicationTrackerCreateDto dto)
    {
        var tracker = new ApplicationTracker
        {
            UserId = dto.UserId,
            JobId = dto.JobId,
            Status = dto.Status,
            FollowUpDate = dto.FollowUpDate,
            AppliedDate = DateTime.Now
        };

        _context.ApplicationTrackers.Add(tracker);
        await _context.SaveChangesAsync();
        return Ok(tracker);
    }
}

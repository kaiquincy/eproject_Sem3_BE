using Microsoft.AspNetCore.Mvc;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployerReviewController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployerReviewController(AppDbContext context) => _context = context;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var review = await _context.EmployerReviews
            .Include(r => r.User)
            .Include(r => r.Job)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (review == null)
            return NotFound();

        return Ok(review);
    }

    // GET: api/employerreview/job/1
    [HttpGet("job/{jobId}")]
    public async Task<IActionResult> GetReviews(int jobId) =>
        Ok(await _context.EmployerReviews
            .Where(r => r.JobId == jobId)
            .ToListAsync());

    // POST: api/employerreview
    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] EmployerReviewCreateDto dto)
    {
        var review = new EmployerReview
        {
            JobId = dto.JobId,
            UserId = dto.UserId,
            Review = dto.Review,
            Rating = dto.Rating,
            DatePosted = DateTime.Now
        };

        _context.EmployerReviews.Add(review);
        await _context.SaveChangesAsync();
        return Ok(review);
    }
}

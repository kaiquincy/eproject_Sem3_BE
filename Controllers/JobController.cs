using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Models;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly AppDbContext _context;

    public JobController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/job
    [HttpGet]
    public async Task<IActionResult> GetAllJobs() =>
        Ok(await _context.Jobs.Include(j => j.Company).ToListAsync());

    // GET: api/job/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob(int id)
    {
        var job = await _context.Jobs
            .Include(j => j.Company)
            .FirstOrDefaultAsync(j => j.Id == id);

        return job == null ? NotFound() : Ok(job);
    }

    // GET: api/job/by-company/{companyId}
    [HttpGet("by-company/{companyId}")]
    public async Task<IActionResult> GetJobsByCompany(int companyId)
    {
        var jobs = await _context.Jobs
            .Where(j => j.CompanyId == companyId)
            .ToListAsync();

        return Ok(jobs);
    }

    // POST: api/job
    [HttpPost]
    public async Task<IActionResult> CreateJob(JobCreateDto dto)
    {
        var job = new Job
        {
            Title = dto.Title,
            Position = dto.Position,
            Location = dto.Location,
            Salary = dto.Salary,
            Description = dto.Description,
            Url = dto.Url,
            CompanyId = dto.CompanyId,
            Category = dto.Category,
            PostedDate = DateTime.Now
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return Ok(job);
    }

    // GET: api/job/by-category/{category}
    [HttpGet("by-category/{category}")]
    public async Task<IActionResult> GetJobsByCategory(string category)
    {
        // chuẩn hoá param về lowercase
        var normalized = category.Trim().ToLower();

        var jobs = await _context.Jobs
            // Include ở đây để EF biết phải join Company
            .Include(j => j.Company)
            // So sánh sau khi ToLower(), EF Core có thể dịch ToLower() sang SQL
            .Where(j => j.Category.ToLower() == normalized)
            .Select(j => new JobWithCompanyNameDto
            {
                Id           = j.Id,
                Title        = j.Title,
                Position     = j.Position,
                Location     = j.Location,
                Salary       = j.Salary,
                Description  = j.Description,
                Url          = j.Url,
                Category     = j.Category,
                PostedDate   = j.PostedDate,
                CompanyName  = j.Company.Name
            })
            .ToListAsync();

        return Ok(jobs);
    }

}



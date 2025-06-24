using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerGuidancePlatform.Models;
using CareerGuidancePlatform.Data;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly AppDbContext _context;

    public CompanyController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ POST /api/company
    [HttpPost]
    public async Task<IActionResult> AddCompany(CompanyCreateDto dto)
    {
        var company = new Company
        {
            Name = dto.Name,
            LogoUrl = dto.LogoUrl,
            Description = dto.Description,
            ContactEmail = dto.ContactEmail
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return Ok(company);
    }

    // ✅ GET /api/company
    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        return Ok(await _context.Companies.ToListAsync());
    }

    // ✅ GET /api/company/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound(); // Trả 404 nếu không tìm thấy
        }
        return Ok(company);
    }
}

// Services/ResumeService.cs
using System.Threading.Tasks;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.DTOs;
using CareerGuidancePlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CareerGuidancePlatform.Services
{
    public class ResumeService : IResumeService
    {
        private readonly AppDbContext _db;
        public ResumeService(AppDbContext db) => _db = db;

        public async Task<int> SaveAsync(int userId, SaveResumeDto dto)
        {
            var resume = new Resume
            {
                UserId = userId,
                Name = dto.Name,
                Content = JsonSerializer.Serialize(dto.Content)
            };
            _db.Resumes.Add(resume);
            await _db.SaveChangesAsync();
            return resume.Id;
        }
        
        public async Task<List<ResumeDto>> ListAsync(int userId)
        {
            return await _db.Resumes
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ResumeDto {
                    Id        = r.Id,
                    Name      = r.Name,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ResumeDetailDto?> GetByIdAsync(int userId, int resumeId)
        {
            var r = await _db.Resumes
                .Where(r => r.UserId == userId && r.Id == resumeId)
                .FirstOrDefaultAsync();
            if (r == null) return null;
            var content = JsonSerializer.Deserialize<object>(r.Content)
                          ?? new object();
            return new ResumeDetailDto {
                Id        = r.Id,
                Name      = r.Name,
                Content   = content,
                CreatedAt = r.CreatedAt
            };
        }
    }
}

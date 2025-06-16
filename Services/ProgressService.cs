using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Models;

namespace CareerGuidancePlatform.Services
{
    public class ProgressService : IProgressService
    {
        private readonly AppDbContext _db;

        public ProgressService(AppDbContext db)
            => _db = db;

        public async Task MarkCompleteAsync(int userId, int stepId)
        {
            var exists = await _db.UserRoadmapProgresses.FindAsync(userId, stepId);
            if (exists == null)
            {
                _db.UserRoadmapProgresses.Add(new UserRoadmapProgress {
                    UserId      = userId,
                    StepId      = stepId,
                    CompletedAt = DateTime.UtcNow
                });
                await _db.SaveChangesAsync();
            }
        }

        public async Task MarkIncompleteAsync(int userId, int stepId)
        {
            var record = await _db.UserRoadmapProgresses.FindAsync(userId, stepId);
            if (record != null)
            {
                _db.UserRoadmapProgresses.Remove(record);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<int>> GetCompletedStepsAsync(int userId)
        {
            var user = await _db.Users
                .Where(u => u.UserId == userId)
                .Select(u => new { u.Career, u.Niche })
                .FirstOrDefaultAsync();

            // 2) Lọc progress theo userId + bộ roadmap tương ứng
            return await _db.UserRoadmapProgresses
                .Where(p => p.UserId == userId)
                .Select(p => p.StepId)
                .ToListAsync();
        }

        public Task MarkCompleteAsync(Guid userId, int stepId)
        {
            throw new NotImplementedException();
        }

        public Task MarkIncompleteAsync(Guid userId, int stepId)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> GetCompletedStepsAsync(Guid userId, string career, string niche)
        {
            throw new NotImplementedException();
        }
    }
}

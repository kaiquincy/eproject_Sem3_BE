using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Dtos;
using CareerGuidancePlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerGuidancePlatform.Services
{
    public class RoadmapService : IRoadmapService
    {
        private readonly AppDbContext _context;

        public RoadmapService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoadmapStepDto>> GetRoadmapAsync(string career, string niche)
        {

            return await _context.RoadmapSteps
                .Where(r => r.Career == career && r.Niche == niche)
                .OrderBy(r => r.Order)
                .Select(r => new RoadmapStepDto
                {
                    Id = r.Id,
                    Order = r.Order,
                    Phase = r.Phase,
                    Title = r.Title,
                    Detail = r.Detail,
                    EstimatedTime = r.EstimatedTime,
                    ResourceLinks = r.ResourceLinks,
                    ResourceTitle = r.ResourceTitle,
                    IsOptional = r.IsOptional
                })
                .ToListAsync();
        }
    }
}

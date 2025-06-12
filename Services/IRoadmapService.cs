using System.Collections.Generic;
using System.Threading.Tasks;
using CareerGuidancePlatform.Dtos;

namespace CareerGuidancePlatform.Services
{
    public interface IRoadmapService
    {
        Task<List<RoadmapStepDto>> GetRoadmapAsync(string career, string niche);
    }
}

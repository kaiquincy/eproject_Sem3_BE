using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CareerGuidancePlatform.Services;
using CareerGuidancePlatform.Models;  // chứa RoadmapStepDto nếu bạn định nghĩa DTO riêng

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Route("api/roadmap")]
    [Authorize]
    public class RoadmapController : ControllerBase
    {
        private readonly IRoadmapService _roadmapService;

        public RoadmapController(IRoadmapService roadmapService)
            => _roadmapService = roadmapService;

        /// <summary>
        /// Lấy danh sách các bước roadmap theo career + niche.
        /// </summary>
        /// <param name="career">Tên ngành nghề (ví dụ "Software Development")</param>
        /// <param name="niche">Tên ngách (ví dụ "Web Developer")</param>
        [HttpGet]
        public async Task<IActionResult> GetRoadmap(
            [FromQuery] string career,
            [FromQuery] string niche)
        {
            if (string.IsNullOrWhiteSpace(career) || string.IsNullOrWhiteSpace(niche))
                return BadRequest("Career và niche là bắt buộc.");

            var steps = await _roadmapService.GetRoadmapAsync(career, niche);
            return Ok(steps);
            // return Ok();
        }
    }
}

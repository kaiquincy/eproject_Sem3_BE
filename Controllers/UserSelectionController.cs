using CareerGuidancePlatform.Dtos;
using CareerGuidancePlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Authorize]
    public class UserSelectionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoadmapService _roadmapService;

        public UserSelectionController(IUserService userService, IRoadmapService roadmapService)
        {
            _userService = userService;
            _roadmapService = roadmapService;
        }

        [HttpPost("api/user/selection")]
        public async Task<IActionResult> SaveSelection([FromBody] UserSelectionDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out var userId)) return Unauthorized();

            await _userService.SaveCareerChoiceAsync(userId, dto.Career, dto.Niche);
            var roadmap = await _roadmapService.GetRoadmapAsync(dto.Career, dto.Niche);
            return Ok(roadmap);
        }
    }
}

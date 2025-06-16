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

        // Helper để parse userId từ claim
        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("User ID claim not found.");
            return int.Parse(claim.Value);
        }

        [HttpPost("api/user/selection")]
        public async Task<IActionResult> SaveSelection([FromBody] UserSelectionDto dto)
        {
            var userId = GetUserId();

            await _userService.SaveCareerChoiceAsync(userId, dto.Career, dto.Niche);
            return Ok(new { message = "success" });
        }
    }
}

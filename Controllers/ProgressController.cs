using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CareerGuidancePlatform.Services;

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Route("api/user/roadmap")]
    [Authorize]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progress;

        public ProgressController(IProgressService progress)
            => _progress = progress;

        // Helper để parse userId từ claim
        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("User ID claim not found.");
            return int.Parse(claim.Value);
        }

        [HttpGet("progress")]
        public async Task<IActionResult> GetProgress()
        {
            var userId = GetUserId();
            var completed = await _progress.GetCompletedStepsAsync(userId);
            return Ok(completed);
        }

        [HttpPost("{stepId}/complete")]
        public async Task<IActionResult> Complete(int stepId)
        {
            var userId = GetUserId();
            await _progress.MarkCompleteAsync(userId, stepId);
            return NoContent();
        }

        [HttpDelete("{stepId}/complete")]
        public async Task<IActionResult> Uncomplete(int stepId)
        {
            var userId = GetUserId();
            await _progress.MarkIncompleteAsync(userId, stepId);
            return NoContent();
        }
    }
}

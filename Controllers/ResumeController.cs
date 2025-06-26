// Controllers/ResumeController.cs
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CareerGuidancePlatform.DTOs;
using CareerGuidancePlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Route("api/resumes")]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumeController(IResumeService resumeService)
            => _resumeService = resumeService;

        private int GetUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(id, out var uid) ? uid : throw new UnauthorizedAccessException();
        }

        [HttpGet]
        public async Task<ActionResult<List<ResumeDto>>> List()
        {
            var userId = GetUserId();
            var list = await _resumeService.ListAsync(userId);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeDetailDto>> Get(int id)
        {
            var userId = GetUserId();
            var detail = await _resumeService.GetByIdAsync(userId, id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveResumeDto dto)
        {
            var userId = GetUserId();
            var newId = await _resumeService.SaveAsync(userId, dto);
            return CreatedAtAction(nameof(Get), new { id = newId }, new { id = newId });
        }
    }
}

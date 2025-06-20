// Controllers/PhaseSubstepsController.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CareerGuidancePlatform.Services;
using CareerGuidancePlatform.Dtos;

[ApiController]
[Route("api/phase-substeps")]
[Authorize]   // nếu bạn muốn chỉ admin/nework mới được tạo
public class PhaseSubstepsController : ControllerBase
{
    private readonly IPhaseSubstepService _substepSvc;

    public PhaseSubstepsController(IPhaseSubstepService substepSvc)
        => _substepSvc = substepSvc;

    /// <summary>
    /// Tạo 1 sub-step mới cho 1 phase (roadmapstep)
    /// POST /api/phase-substeps
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePhaseSubstepDto dto)
    {
        // (Optional) validate dto.RoadmapStepId tồn tại, dto.StepOrder trong 1..6, v.v.
        var created = await _substepSvc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByRoadmapStep),
            new { roadmapStepId = created.RoadmapstepId },
            created);
    }

    /// <summary>
    /// Lấy tất cả sub-steps cho 1 roadmapstep
    /// GET /api/phase-substeps?roadmapStepId=123
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetByRoadmapStep([FromQuery] int roadmapStepId)
    {
        var list = await _substepSvc.GetByRoadmapStepAsync(roadmapStepId);
        return Ok(list);
    }
}

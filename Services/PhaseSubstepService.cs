// Services/PhaseSubstepService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Models;
using CareerGuidancePlatform.Dtos;

public class PhaseSubstepService : IPhaseSubstepService
{
    private readonly AppDbContext _db;

    public PhaseSubstepService(AppDbContext db) => _db = db;

    public async Task<PhaseSubstep> CreateAsync(CreatePhaseSubstepDto dto)
    {
        var entity = new PhaseSubstep
        {
            RoadmapstepId = dto.RoadmapStepId,
            StepOrder     = dto.StepOrder,
            Title         = dto.Title,
            Detail        = dto.Detail,
            EstimatedTime = dto.EstimatedTime,
            ResourceUrl   = dto.ResourceUrl,
            ResourceTitle = dto.ResourceTitle,
            IsOptional    = dto.IsOptional
        };

        _db.PhaseSubsteps.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<PhaseSubstep>> GetByRoadmapStepAsync(int roadmapStepId)
    {
        return await _db.PhaseSubsteps
            .AsNoTracking()
            .Where(s => s.RoadmapstepId == roadmapStepId)
            .OrderBy(s => s.StepOrder)
            .ToListAsync();
    }
}

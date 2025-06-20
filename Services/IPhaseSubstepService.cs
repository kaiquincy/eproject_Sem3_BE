// Services/IPhaseSubstepService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerGuidancePlatform.Models;
using CareerGuidancePlatform.Dtos;

public interface IPhaseSubstepService
{
    Task<PhaseSubstep> CreateAsync(CreatePhaseSubstepDto dto);
    Task<IEnumerable<PhaseSubstep>> GetByRoadmapStepAsync(int roadmapStepId);
}

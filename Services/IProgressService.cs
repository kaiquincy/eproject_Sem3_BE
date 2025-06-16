using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerGuidancePlatform.Services
{
    public interface IProgressService
    {
        Task MarkCompleteAsync(int userId, int stepId);
        Task MarkIncompleteAsync(int userId, int stepId);
        Task<List<int>> GetCompletedStepsAsync(int userId);
    }
}

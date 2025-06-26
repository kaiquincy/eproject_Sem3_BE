// Services/IResumeService.cs
using System.Threading.Tasks;
using CareerGuidancePlatform.DTOs;

namespace CareerGuidancePlatform.Services
{
    public interface IResumeService
    {
        Task<int> SaveAsync(int userId, SaveResumeDto dto);
        Task<List<ResumeDto>>        ListAsync(int userId);
        Task<ResumeDetailDto?>       GetByIdAsync(int userId, int resumeId);
    }
}

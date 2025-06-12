using System.Threading.Tasks;

namespace CareerGuidancePlatform.Services
{
    public interface IUserService
    {
        Task SaveCareerChoiceAsync(int userId, string career, string niche);
    }
}

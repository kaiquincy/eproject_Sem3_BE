using CareerGuidancePlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerGuidancePlatform.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveCareerChoiceAsync(int userId, string career, string niche)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return;

            user.Career = career;
            user.Niche = niche;
            await _context.SaveChangesAsync();
        }
    }
}

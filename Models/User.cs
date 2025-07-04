using System.ComponentModel.DataAnnotations;

namespace CareerGuidancePlatform.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public string Career { get; set; } = string.Empty;

        public string Niche { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public ICollection<UserRoadmapProgress> RoadmapProgresses { get; set; }
    }
}

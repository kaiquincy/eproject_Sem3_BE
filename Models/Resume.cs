// Models/Resume.cs
using System;

namespace CareerGuidancePlatform.Models
{
    public class Resume
    {
        public int    Id        { get; set; }
        public int    UserId    { get; set; }
        public string Name      { get; set; } = null!;
        public string Content   { get; set; } = null!;  // JSON string of the CV
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

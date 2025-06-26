using System.ComponentModel.DataAnnotations;

namespace CareerGuidancePlatform.Models
{

    public enum MentorStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public class Mentor
    {
        public int MentorId { get; set; }

        public int UserId { get; set; } // khóa ngoại      

        public string Career { get; set; } = string.Empty;

        public string Niche { get; set; } = string.Empty;

        public string Availability { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public MentorStatus Status { get; set; }    // thêm
        public DateTime RequestedAt { get; set; }   // thêm

        public User? User { get; set; }
    }
}

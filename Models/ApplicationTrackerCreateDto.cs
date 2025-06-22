namespace CareerGuidancePlatform.Models
{
    public class ApplicationTrackerCreateDto
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string Status { get; set; }
        public DateTime FollowUpDate { get; set; }
    }
}

namespace CareerGuidancePlatform.Models
{
    public class EmployerReviewCreateDto
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}

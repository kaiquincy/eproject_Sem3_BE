using System.Text.Json.Serialization;

namespace CareerGuidancePlatform.Models
{
    public class ApplicationTracker
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int JobId { get; set; }

        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime? FollowUpDate { get; set; }

        [JsonIgnore]
        public Job Job { get; set; }
    }
}

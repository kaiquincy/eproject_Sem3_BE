namespace CareerGuidancePlatform.Models
{
    public class EmployerReview
    {
        public int Id { get; set; }

        public int JobId { get; set; }
        public Job? Job { get; set; }  // ✅ Nullable để không yêu cầu khi POST

        public int UserId { get; set; }
        public User? User { get; set; }  // ✅ Nullable để không yêu cầu khi POST

        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
    }


}

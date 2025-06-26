namespace CareerGuidancePlatform.DTOs
{
    public class ResumeDto
    {
        public int      Id        { get; set; }
        public string   Name      { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
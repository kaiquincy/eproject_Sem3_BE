namespace CareerGuidancePlatform.DTOs
{
    public class ResumeDetailDto
    {
        public int      Id      { get; set; }
        public string   Name    { get; set; } = null!;
        public object   Content { get; set; } = null!;  // deserialized CV object
        public DateTime CreatedAt { get; set; }
    }
}
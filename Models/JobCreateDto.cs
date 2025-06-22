namespace CareerGuidancePlatform.Models
{
    public class JobCreateDto
    {
        public string Title { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int CompanyId { get; set; }
        public string Category { get; set; }
    }
}

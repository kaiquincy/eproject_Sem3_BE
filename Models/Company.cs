namespace CareerGuidancePlatform.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}

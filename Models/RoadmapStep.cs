using System.Collections.Generic;

namespace CareerGuidancePlatform.Models
{
    public class RoadmapStep
    {
        public int Id { get; set; }
        public string Career { get; set; } = string.Empty;
        public string Niche { get; set; } = string.Empty;
        public int Order { get; set; }
        public string Phase { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string EstimatedTime { get; set; } = string.Empty;
        public List<string> ResourceLinks { get; set; } = new();
        public bool IsOptional { get; set; }
    }
}

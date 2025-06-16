using System.Collections.Generic;
using CareerGuidancePlatform.Dtos;

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
        public string ResourceLinks { get; set; } = string.Empty;

        public string ResourceTitle { get; set; } = string.Empty;

        public bool IsOptional { get; set; }
    }
}

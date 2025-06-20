using System.Collections.Generic;

namespace CareerGuidancePlatform.Dtos
{
    public class RoadmapStepDto
    {
        public int Id  { get; set; }
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

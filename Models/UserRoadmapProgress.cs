using System;

namespace CareerGuidancePlatform.Models
{
    public class UserRoadmapProgress
    {
        public int UserId        { get; set; }
        public int  StepId        { get; set; }
        public DateTime CompletedAt { get; set; }

        // Navigation properties
        public User         User        { get; set; }
        public RoadmapStep  RoadmapStep { get; set; }
    }
}

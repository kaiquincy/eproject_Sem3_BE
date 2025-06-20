// Models/PhaseSubstep.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerGuidancePlatform.Models
{
    [Table("phase_substeps")]
    public class PhaseSubstep
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RoadmapStep")]
        public int RoadmapstepId { get; set; }

        public int StepOrder { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        public string Detail { get; set; }
        [MaxLength(50)]
        public string EstimatedTime { get; set; }
        public string ResourceUrl { get; set; }
        public string ResourceTitle { get; set; }
        public bool IsOptional { get; set; }

        // Navigation trở lại RoadmapStep
        public RoadmapStep RoadmapStep;

        // Collection navigation để track progress
        public ICollection<UserRoadmapProgress> UserProgress;
        
    }
}

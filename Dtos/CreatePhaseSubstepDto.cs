// Dtos/CreatePhaseSubstepDto.cs
public class CreatePhaseSubstepDto
{
    public int RoadmapStepId { get; set; }    // FK về bảng roadmapsteps
    public int StepOrder { get; set; }        // số thứ tự (1..6)
    public string Title { get; set; }
    public string Detail { get; set; }
    public string EstimatedTime { get; set; }
    public string ResourceUrl { get; set; }
    public string ResourceTitle { get; set; }
    public bool IsOptional { get; set; }
}

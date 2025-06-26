// DTOs/SaveResumeDto.cs
namespace CareerGuidancePlatform.DTOs
{
    public class SaveResumeDto
    {
        public string Name    { get; set; } = null!;
        public object Content { get; set; } = null!;  // will be serialized
    }
}

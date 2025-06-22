using System.ComponentModel.DataAnnotations;

namespace CareerGuidancePlatform.Entities
{
    public class UserCalendar
    {
        [Key]
        public string UserId { get; set; }              // PK = userId từ token

        [Required]
        public string ScheduleJson { get; set; }        // JSON lưu trữ Dictionary<string, List<int>>
    }
}


public class MentorWithUserDto
{
    // Thông tin mentor
    public int MentorId { get; set; }
    public string Career { get; set; } = string.Empty;
    public string Niche { get; set; } = string.Empty;
    public string Availability { get; set; } = string.Empty;
    // … những field khác của Mentor nếu cần

    // Thông tin user đính kèm
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Email { get; set; }

    public string FullName { get; set; } = string.Empty;
        // … thêm các trường bạn muốn expose
}
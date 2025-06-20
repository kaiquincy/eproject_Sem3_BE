public class ConversationDto
{
    public int PartnerId { get; set; }
    public string PartnerName { get; set; }
    public string AvatarUrl   { get; set; }

    public string ParnerRole { get; set; }

    public string PartnerUsername { get; set; }

    public string LastMessage { get; set; }
    public DateTime LastTimestamp { get; set; }
    public int UnreadCount    { get; set; }
}

namespace NotificationPlatform.Application.DTOs.Notifications;

public class NotificationResponse
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string? Subject { get; set; }
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
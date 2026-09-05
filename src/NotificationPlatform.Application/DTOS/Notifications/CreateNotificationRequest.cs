namespace NotificationPlatform.Application.DTOs.Notifications;

public class CreateNotificationRequest
{
    public string UserId { get; set; } = default!;
    public int TemplateId { get; set; }
}
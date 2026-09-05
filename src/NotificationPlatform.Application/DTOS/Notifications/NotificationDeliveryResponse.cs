namespace NotificationPlatform.Application.DTOs.Notifications;

public class NotificationDeliveryResponse
{
    public int Id { get; set; }
    public int NotificationId { get; set; }
    public string Channel { get; set; } = default!;
    public string Status { get; set; } = default!;
    public int RetryCount { get; set; }
    public DateTime? SentAt { get; set; }
    public string? ErrorMessage { get; set; }
}
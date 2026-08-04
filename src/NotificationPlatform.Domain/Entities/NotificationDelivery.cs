using NotificationPlatform.Domain.Enums;

namespace NotificationPlatform.Domain.Entities
{
    public class NotificationDelivery 
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; } = default!;
        public NotificationChannel Channel { get; set; }
        public DeliveryStatus Status { get; set; }
        public int RetryCount { get; set; }
        public DateTime? SentAt { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
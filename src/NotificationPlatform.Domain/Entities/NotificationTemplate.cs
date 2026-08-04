using NotificationPlatform.Domain.Enums;

namespace NotificationPlatform.Domain.Entities
{
    public class NotificationTemplate
    {
        public int Id { get; set; }
        public int EventId { get; set; } //fk
        public Event Event { get; set; } = default!;
        public NotificationChannel Channel { get; set; }
        public string? Subject { get; set; }
        public string Body { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation Property
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
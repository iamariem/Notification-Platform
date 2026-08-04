namespace NotificationPlatform.Domain.Entities
{
    public class Notification 
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public int TemplateId { get; set; }
        public NotificationTemplate Template { get; set; } = default!;
        public string? Subject { get; set; }
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        //Navigation Property
        public ICollection<NotificationDelivery> Deliveries { get; set; } = new List<NotificationDelivery>();
    }
}
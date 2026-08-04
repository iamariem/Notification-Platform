using NotificationPlatform.Domain.Enums;

namespace NotificationPlatform.Domain.Entities
{
    public class Event 
    {
        public int Id { get; set; }
        public EventType Type { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        //Navigation Property
        public ICollection<NotificationTemplate> NotificationTemplates { get; set; } = new List<NotificationTemplate>();
    }
}
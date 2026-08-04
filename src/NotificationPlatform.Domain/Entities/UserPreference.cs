using Microsoft.AspNetCore.Identity;

namespace NotificationPlatform.Domain.Entities
{
    public class UserPreference 
    {
        public int Id { get; set; }
        public String UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public bool EmailEnabled { get; set; } = true;
        public bool PushEnabled { get; set; } = true;
        public bool SmsEnabled { get; set; } = true;
        public TimeOnly? QuietHoursStart { get; set; }
        public TimeOnly? QuietHoursEnd { get; set; }
    }
}
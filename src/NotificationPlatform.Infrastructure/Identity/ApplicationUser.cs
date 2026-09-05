using Microsoft.AspNetCore.Identity;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrastructure.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
   
   //Navigation Property
    public UserPreference? UserPreference { get; set; }
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
using Microsoft.AspNetCore.Identity.EntityFrameWorkCore;
using Microsoft.EntityFrameWorkCore;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrasructure.Persistence.Context
{
    public class ApplicationUser : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public Dbset<Event> Events { get; set; }
        public Dbset<Notification> Notifications { get; set; }
        public Dbset<NotificationTemplate> NotificationTemplates { get; set; }
        public Dbset<NotificationDelivery> NotificationDeliveries { get; set; }
        public Dbset<UserPreference> UserPreferences { get; set; }

        protected override void onModelCreating(ModelBuilder builder)
        {
            base.onModelCreating(builder);
            builder.ApplyConfigurationFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
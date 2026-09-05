using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotificationPlatform.Domain.Entities;
using NotificationPlatform.Infrastructure.Entities;

namespace NotificationPlatform.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; } = null!;
        public DbSet<NotificationDelivery> NotificationDeliveries { get; set; } = null!;
        public DbSet<UserPreference> UserPreferences { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}
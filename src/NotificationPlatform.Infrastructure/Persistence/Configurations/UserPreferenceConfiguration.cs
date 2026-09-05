using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationPlatform.Domain.Entities;
using NotificationPlatform.Infrastructure.Entities;

namespace NotificationPlatform.Infrastructure.Persistence.Configurations;

public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
{
    public void Configure(EntityTypeBuilder<UserPreference> builder)
    {
        builder.ToTable("UserPreferences");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.EmailEnabled)
               .HasDefaultValue(true);

        builder.Property(p => p.SmsEnabled)
               .HasDefaultValue(true);

        builder.Property(p => p.PushEnabled)
               .HasDefaultValue(true);

        builder.HasOne<ApplicationUser>()
               .WithOne(u => u.UserPreference)
               .HasForeignKey<UserPreference>(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
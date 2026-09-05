using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrastructure.Persistence.Configurations;

public class NotificationDeliveryConfiguration : IEntityTypeConfiguration<NotificationDelivery>
{
    public void Configure(EntityTypeBuilder<NotificationDelivery> builder)
    {
        builder.ToTable("NotificationDeliveries");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Channel)
               .IsRequired();

        builder.Property(d => d.Status)
               .IsRequired();

        builder.Property(d => d.RetryCount)
               .HasDefaultValue(0);

        builder.Property(d => d.ErrorMessage)
               .HasMaxLength(500);

        builder.HasOne(d => d.Notification)
               .WithMany(n => n.Deliveries)
               .HasForeignKey(d => d.NotificationId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Subject)
               .HasMaxLength(200);

        builder.Property(n => n.Body)
               .IsRequired();

        builder.Property(n => n.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(n => n.User)
               .WithMany(u => u.Notifications)
               .HasForeignKey(n => n.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(n => n.Template)
               .WithMany(t => t.Notifications)
               .HasForeignKey(n => n.TemplateId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
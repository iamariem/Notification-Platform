using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrastructure.Persistence.Configurations;

public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
{
    public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
    {
        builder.ToTable("NotificationTemplates");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Subject)
               .HasMaxLength(200);

        builder.Property(t => t.Body)
               .IsRequired();

        builder.Property(t => t.IsActive)
               .HasDefaultValue(true);

        builder.Property(t => t.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(t => t.Event)
               .WithMany(e => e.NotificationTemplates)
               .HasForeignKey(t => t.EventId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
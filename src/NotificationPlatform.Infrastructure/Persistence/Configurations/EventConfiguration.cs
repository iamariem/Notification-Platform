using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Type)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasMaxLength(500);

            builder.Property(e => e.IsActive)
                   .HasDefaultValue(true);

            builder.Property(e => e.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(e => e.NotificationTemplates)
                   .WithOne(t => t.Event)
                   .HasForeignKey(t => t.EventId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
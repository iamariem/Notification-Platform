using Microsoft.EntityFrameworkCore;
using NotificationPlatform.Application.Interfaces;
using NotificationPlatform.Domain.Entities;
using NotificationPlatform.Infrastructure.Persistence.Context;

namespace NotificationPlatform.Infrastructure.Data;

public class NotificationData : INotificationData
{
    private readonly ApplicationDbContext _context;

    public NotificationData(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationTemplate?> GetTemplateByIdAsync(
        int templateId,
        CancellationToken cancellationToken = default)
    {
        return await _context.NotificationTemplates
            .FirstOrDefaultAsync(
                x => x.Id == templateId,
                cancellationToken);
    }

    public async Task<Notification?> GetNotificationByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AddNotificationAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        await _context.Notifications.AddAsync(
            notification,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddDeliveryAsync(
        NotificationDelivery delivery,
        CancellationToken cancellationToken = default)
    {
        await _context.NotificationDeliveries.AddAsync(
            delivery,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
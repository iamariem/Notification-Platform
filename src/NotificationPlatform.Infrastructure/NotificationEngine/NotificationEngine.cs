using Microsoft.EntityFrameworkCore;
using NotificationPlatform.Domain.Entities;
using NotificationPlatform.Domain.Enums;
using NotificationPlatform.Infrastructure.NotificationProviders;
using NotificationPlatform.Infrastructure.Persistence.Context;

namespace NotificationPlatform.Infrastructure.NotificationEngine;

public class NotificationEngine
{
    private const int MaxRetryCount = 3;

    private readonly ApplicationDbContext _context;
    private readonly NotificationSenderFactory _senderFactory;

    public NotificationEngine(
        ApplicationDbContext context,
        NotificationSenderFactory senderFactory)
    {
        _context = context;
        _senderFactory = senderFactory;
    }

    public async Task SendAsync(
        int notificationId,
        CancellationToken cancellationToken = default)
    {
        var notification = await _context.Notifications
            .Include(n => n.Template)
            .Include(n => n.Deliveries)
            .FirstOrDefaultAsync(
                n => n.Id == notificationId,
                cancellationToken);

        if (notification is null)
        {
            throw new KeyNotFoundException(
                $"Notification with ID {notificationId} was not found.");
        }

        var channel = notification.Template.Channel;

        var delivery = new NotificationDelivery
        {
            NotificationId = notification.Id,
            Channel = channel,
            Status = DeliveryStatus.Pending,
            RetryCount = 0
        };

        await _context.NotificationDeliveries.AddAsync(
            delivery,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var sender = _senderFactory.GetSender(channel);

        for (var attempt = 1; attempt <= MaxRetryCount; attempt++)
        {
            try
            {
                delivery.RetryCount = attempt;

                await sender.SendAsync(
                    notification,
                    cancellationToken);

                delivery.Status = DeliveryStatus.Sent;
                delivery.SentAt = DateTime.UtcNow;
                delivery.ErrorMessage = null;

                await _context.SaveChangesAsync(cancellationToken);

                return;
            }
            catch (Exception ex)
            {
                delivery.Status = DeliveryStatus.Failed;
                delivery.ErrorMessage = ex.Message;

                await _context.SaveChangesAsync(cancellationToken);

                Console.WriteLine(
                    $"[RETRY] Attempt {attempt}/{MaxRetryCount} failed.");

                if (attempt < MaxRetryCount)
                {
                    await Task.Delay(
                        TimeSpan.FromSeconds(2),
                        cancellationToken);
                }
            }
        }

        Console.WriteLine(
            $"[FAILED] Notification {notificationId} failed after {MaxRetryCount} attempts.");
    }
}
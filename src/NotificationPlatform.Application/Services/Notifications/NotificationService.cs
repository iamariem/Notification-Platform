using NotificationPlatform.Application.DTOs.Notifications;
using NotificationPlatform.Application.Interfaces;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Application.Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly INotificationData _notificationData;

    public NotificationService(INotificationData notificationData)
    {
        _notificationData = notificationData;
    }

    public async Task<NotificationResponse> CreateAsync(
        CreateNotificationRequest request,
        CancellationToken cancellationToken = default)
    {
        var template = await _notificationData.GetTemplateByIdAsync(
            request.TemplateId,
            cancellationToken);

        if (template is null)
        {
            throw new KeyNotFoundException(
                $"Notification template with ID {request.TemplateId} was not found.");
        }

        if (!template.IsActive)
        {
            throw new InvalidOperationException(
                "The selected notification template is not active.");
        }

        var notification = new Notification
        {
            UserId = request.UserId,
            TemplateId = template.Id,
            Subject = template.Subject,
            Body = template.Body,
            CreatedAt = DateTime.UtcNow
        };

        await _notificationData.AddNotificationAsync(
            notification,
            cancellationToken);

        return new NotificationResponse
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Subject = notification.Subject,
            Body = notification.Body,
            CreatedAt = notification.CreatedAt
        };
    }

    public async Task<NotificationResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var notification = await _notificationData.GetNotificationByIdAsync(
            id,
            cancellationToken);

        if (notification is null)
        {
            return null;
        }

        return new NotificationResponse
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Subject = notification.Subject,
            Body = notification.Body,
            CreatedAt = notification.CreatedAt
        };
    }
}
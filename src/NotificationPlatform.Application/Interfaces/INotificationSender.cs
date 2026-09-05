using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Application.Interfaces;

public interface INotificationSender
{
    Task SendAsync(
        Notification notification,
        CancellationToken cancellationToken = default);
}
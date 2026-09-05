using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Application.Interfaces;

public interface INotificationData
{
    Task<NotificationTemplate?> GetTemplateByIdAsync(
        int templateId,
        CancellationToken cancellationToken = default);

    Task<Notification?> GetNotificationByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task AddNotificationAsync(
        Notification notification,
        CancellationToken cancellationToken = default);

    Task AddDeliveryAsync(
        NotificationDelivery delivery,
        CancellationToken cancellationToken = default);
}
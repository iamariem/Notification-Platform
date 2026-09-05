using NotificationPlatform.Application.DTOs.Notifications;

namespace NotificationPlatform.Application.Interfaces;

public interface INotificationService
{
    Task<NotificationResponse> CreateAsync(
        CreateNotificationRequest request,
        CancellationToken cancellationToken = default);

    Task<NotificationResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}
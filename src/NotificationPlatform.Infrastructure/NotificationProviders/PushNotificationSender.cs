using NotificationPlatform.Application.Interfaces;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrastructure.NotificationProviders;

public class PushNotificationSender : INotificationSender
{
    public async Task SendAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine(
            $"[PUSH] Sending notification to user {notification.UserId}");

        Console.WriteLine(
            $"Message: {notification.Body}");

        await Task.CompletedTask;
    }
}
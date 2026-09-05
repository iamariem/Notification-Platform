using NotificationPlatform.Application.Interfaces;
using NotificationPlatform.Domain.Entities;

namespace NotificationPlatform.Infrastructure.NotificationProviders;

public class EmailNotificationSender : INotificationSender
{
    public async Task SendAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine(
            $"[EMAIL] Sending notification to user {notification.UserId}");

        Console.WriteLine(
            $"Subject: {notification.Subject}");

        Console.WriteLine(
            $"Body: {notification.Body}");

        await Task.CompletedTask;
    }
}
using NotificationPlatform.Application.Interfaces;
using NotificationPlatform.Domain.Enums;

namespace NotificationPlatform.Infrastructure.NotificationProviders;

public class NotificationSenderFactory
{
    private readonly EmailNotificationSender _emailSender;
    private readonly SmsNotificationSender _smsSender;
    private readonly PushNotificationSender _pushSender;

    public NotificationSenderFactory(
        EmailNotificationSender emailSender,
        SmsNotificationSender smsSender,
        PushNotificationSender pushSender)
    {
        _emailSender = emailSender;
        _smsSender = smsSender;
        _pushSender = pushSender;
    }

    public INotificationSender GetSender(NotificationChannel channel)
    {
        return channel switch
        {
            NotificationChannel.Email => _emailSender,
            NotificationChannel.Sms => _smsSender,
            NotificationChannel.Push => _pushSender,

            _ => throw new ArgumentOutOfRangeException(
                nameof(channel),
                channel,
                "Unsupported notification channel.")
        };
    }
}
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace NotificationPlatform.Infrastructure.Messaging;

public class RabbitMqPublisher
{
    private const string QueueName = "notification-queue";

    public async Task PublishAsync(
        int notificationId,
        CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };

        await using var connection =
            await factory.CreateConnectionAsync(cancellationToken);

        await using var channel =
            await connection.CreateChannelAsync(
                cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        var message = JsonSerializer.Serialize(new
        {
            NotificationId = notificationId
        });

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: QueueName,
            body: body,
            cancellationToken: cancellationToken);
    }
}
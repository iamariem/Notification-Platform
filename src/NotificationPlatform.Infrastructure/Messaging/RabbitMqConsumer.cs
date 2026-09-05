using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationPlatform.Infrastructure.NotificationEngine;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationPlatform.Infrastructure.Messaging;

public class RabbitMqConsumer : BackgroundService
{
    private const string QueueName = "notification-queue";

    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMqConsumer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };

        await using var connection =
            await factory.CreateConnectionAsync(stoppingToken);

        await using var channel =
            await connection.CreateChannelAsync(
                cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, args) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(
                    args.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<NotificationMessage>(json);

                if (message is null)
                {
                    await channel.BasicNackAsync(
                        args.DeliveryTag,
                        false,
                        false);

                    return;
                }

                using var scope =
                    _scopeFactory.CreateScope();

                var engine =
                    scope.ServiceProvider
                        .GetRequiredService<
                            NotificationPlatform.Infrastructure.NotificationEngine.NotificationEngine>();

                await engine.SendAsync(
                    message.NotificationId,
                    stoppingToken);

                await channel.BasicAckAsync(
                    args.DeliveryTag,
                    false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"RabbitMQ Consumer Error: {ex.Message}");

                await channel.BasicNackAsync(
                    args.DeliveryTag,
                    false,
                    false);
            }
        };

        await channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken);
    }

    private class NotificationMessage
    {
        public int NotificationId { get; set; }
    }
}
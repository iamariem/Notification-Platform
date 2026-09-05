using Microsoft.AspNetCore.Mvc;
using NotificationPlatform.Application.DTOs.Notifications;
using NotificationPlatform.Application.Interfaces;
using NotificationPlatform.Infrastructure.Messaging;

namespace NotificationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly RabbitMqPublisher _publisher;

    public NotificationsController(
        INotificationService notificationService,
        RabbitMqPublisher publisher)
    {
        _notificationService = notificationService;
        _publisher = publisher;
    }

    [HttpPost]
    public async Task<ActionResult<NotificationResponse>> Create(
        CreateNotificationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _notificationService.CreateAsync(
            request,
            cancellationToken);

        await _publisher.PublishAsync(
            result.Id,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NotificationResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _notificationService.GetByIdAsync(
            id,
            cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
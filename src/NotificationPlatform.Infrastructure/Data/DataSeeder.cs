using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotificationPlatform.Domain.Entities;
using NotificationPlatform.Domain.Enums;
using NotificationPlatform.Infrastructure.Entities;
using NotificationPlatform.Infrastructure.Persistence.Context;

namespace NotificationPlatform.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        await context.Database.MigrateAsync();

        // =========================
        // Users
        // =========================

        var user = await userManager.FindByEmailAsync("test@notificationplatform.com");

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = "test@notificationplatform.com",
                Email = "test@notificationplatform.com",
                FirstName = "Test",
                LastName = "User",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                user,
                "Test@12345");

            if (!result.Succeeded)
            {
                throw new Exception(
                    $"Failed to create test user: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // =========================
        // User Preference
        // =========================

        var preferenceExists = await context.UserPreferences
            .AnyAsync(x => x.UserId == user.Id);

        if (!preferenceExists)
        {
            context.UserPreferences.Add(new UserPreference
            {
                UserId = user.Id,
                EmailEnabled = true,
                SmsEnabled = true,
                PushEnabled = true
            });

            await context.SaveChangesAsync();
        }

        // =========================
        // Events
        // =========================

        if (!await context.Events.AnyAsync())
        {
            context.Events.AddRange(
                new Event
                {
                    Type = EventType.UserRegistered,
                    Description = "Triggered when a new user registers.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Event
                {
                    Type = EventType.OrderPlaced,
                    Description = "Triggered when a new order is placed.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Event
                {
                    Type = EventType.OrderShipped,
                    Description = "Triggered when an order is shipped.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Event
                {
                    Type = EventType.PasswordReset,
                    Description = "Triggered when a user requests a password reset.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Event
                {
                    Type = EventType.PaymentSucceeded,
                    Description = "Triggered when a payment succeeds.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();
        }

        // =========================
        // Notification Templates
        // =========================

        if (!await context.NotificationTemplates.AnyAsync())
        {
            var events = await context.Events
                .ToDictionaryAsync(x => x.Type, x => x.Id);

            context.NotificationTemplates.AddRange(
                // User Registered
                new NotificationTemplate
                {
                    EventId = events[EventType.UserRegistered],
                    Channel = NotificationChannel.Email,
                    Subject = "Welcome to NotificationPlatform",
                    Body = "Welcome! Your account has been created successfully.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new NotificationTemplate
                {
                    EventId = events[EventType.UserRegistered],
                    Channel = NotificationChannel.Push,
                    Subject = "Welcome!",
                    Body = "Welcome to NotificationPlatform.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },

                // Order Placed
                new NotificationTemplate
                {
                    EventId = events[EventType.OrderPlaced],
                    Channel = NotificationChannel.Email,
                    Subject = "Order Placed",
                    Body = "Your order has been placed successfully.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new NotificationTemplate
                {
                    EventId = events[EventType.OrderPlaced],
                    Channel = NotificationChannel.Sms,
                    Subject = "Order Placed",
                    Body = "Your order has been placed successfully.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },

                // Order Shipped
                new NotificationTemplate
                {
                    EventId = events[EventType.OrderShipped],
                    Channel = NotificationChannel.Email,
                    Subject = "Your Order Has Shipped",
                    Body = "Your order is on its way.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },

                // Password Reset
                new NotificationTemplate
                {
                    EventId = events[EventType.PasswordReset],
                    Channel = NotificationChannel.Email,
                    Subject = "Password Reset",
                    Body = "Your password reset request has been received.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },

                // Payment Succeeded
                new NotificationTemplate
                {
                    EventId = events[EventType.PaymentSucceeded],
                    Channel = NotificationChannel.Email,
                    Subject = "Payment Successful",
                    Body = "Your payment was completed successfully.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();
        }
    }
}
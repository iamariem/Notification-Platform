using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotificationPlatform.Application.Interfaces;
using NotificationPlatform.Application.Services.Notifications;
using NotificationPlatform.Infrastructure.Data;
using NotificationPlatform.Infrastructure.Entities;
using NotificationPlatform.Infrastructure.Messaging;
using NotificationPlatform.Infrastructure.Persistence.Context;
using NotificationPlatform.Infrastructure.NotificationEngine;
using NotificationPlatform.Infrastructure.NotificationProviders;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Identity
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Application Services
builder.Services.AddScoped<INotificationData, NotificationData>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Notification Providers
builder.Services.AddScoped<EmailNotificationSender>();
builder.Services.AddScoped<SmsNotificationSender>();
builder.Services.AddScoped<PushNotificationSender>();
builder.Services.AddScoped<NotificationSenderFactory>();

// Notification Engine
builder.Services.AddScoped<NotificationEngine>();

// RabbitMQ
builder.Services.AddScoped<RabbitMqPublisher>();
builder.Services.AddHostedService<RabbitMqConsumer>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Data Seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    await DataSeeder.SeedAsync(context, userManager);
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
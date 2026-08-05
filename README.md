# Notification Platform

A scalable, event-driven notification platform built with ASP.NET Core, designed to deliver notifications through multiple channels including Email, SMS, and Push Notifications. The platform follows Clean Architecture principles to ensure maintainability, scalability, and extensibility.

## Features

- Event-driven notification processing
- Multi-channel notifications (Email, SMS, Push)
- Notification templates with dynamic placeholders
- User notification preferences
- Delivery tracking
- Background processing
- Retry mechanism for failed notifications
- Secure authentication using ASP.NET Identity & JWT
- Strategy Pattern for notification providers
- Docker support

## Architecture

The project follows **Clean Architecture**.

```
NotificationPlatform
│
├── NotificationPlatform.API
├── NotificationPlatform.Application
├── NotificationPlatform.Domain
└── NotificationPlatform.Infrastructure
```

## Tech Stack

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT Authentication
- RabbitMQ
- MailKit
- Docker

## Project Status

🚧 Currently under active development.

## Future Enhancements

- SMS Provider Integration
- Push Notification Provider
- Notification Scheduling
- Dashboard & Analytics
- Logging with Serilog
- Unit Testing
- CI/CD Pipeline

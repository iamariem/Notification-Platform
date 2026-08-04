# Notification Platform

A scalable and extensible notification platform built with **ASP.NET Core** and **Clean Architecture**. The system enables applications to manage notification templates, process events, and deliver notifications through multiple channels such as Email, SMS, and Push Notifications.

## Features

- Event-driven notification processing
- Notification templates
- Multi-channel delivery (Email, SMS, Push)
- User notification preferences
- Retry mechanism for failed deliveries
- Delivery status tracking
- Quiet hours support
- Background processing (coming soon)

---

## Architecture

The project follows **Clean Architecture**.

```text
NotificationPlatform.sln

├── NotificationPlatform.API
├── NotificationPlatform.Application
├── NotificationPlatform.Domain
└── NotificationPlatform.Infrastructure
```

---

## Tech Stack

- ASP.NET Core
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- Clean Architecture
- REST API

---

## Domain Model

Current domain entities:

- ApplicationUser
- Event
- NotificationTemplate
- Notification
- NotificationDelivery
- UserPreference

---

## Project Status

🚧 Under Development

This project is currently under active development.

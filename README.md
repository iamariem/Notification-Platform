# NotificationPlatform

An event-driven notification platform built with **ASP.NET Core .NET 10**, supporting **Email, SMS, and Push Notifications**.

The system follows **Clean Architecture** and uses the **Strategy Pattern** and **RabbitMQ** for asynchronous notification processing.

---

## Features

- Email, SMS, and Push Notifications
- Asynchronous processing with RabbitMQ
- Retry mechanism with up to 3 attempts
- Strategy Pattern for notification providers
- ASP.NET Core Identity
- SQL Server with Entity Framework Core
- FluentValidation
- Swagger
- Docker
- Dependency Injection
- Background notification processing

---

## Architecture

![NotificationPlatform Architecture](docs/architecture.png)

### Project Structure

```text
NotificationPlatform
│
├── src
│   ├── NotificationPlatform.Domain
│   │   ├── Entities
│   │   └── Enums
│   │
│   ├── NotificationPlatform.Application
│   │   ├── DTOs
│   │   ├── Interfaces
│   │   ├── Services
│   │   └── Validators
│   │
│   ├── NotificationPlatform.Infrastructure
│   │   ├── Data
│   │   ├── Identity
│   │   ├── Messaging
│   │   ├── NotificationEngine
│   │   ├── NotificationProviders
│   │   └── Persistence
│   │
│   └── NotificationPlatform.API
│       ├── Controllers
│       └── Program.cs
│
├── docs
│   └── architecture.png
│
├── .gitignore
├── NotificationPlatform.slnx
└── README.md
```

---

## Notification Flow

```text
Client
   │
   ▼
API Layer
   │
   ▼
Application Layer
   │
   ▼
SQL Server
   │
   ▼
RabbitMQ
   │
   ▼
Consumer
   │
   ▼
NotificationEngine
   │
   ▼
Strategy Factory
   │
   ├──► Email
   ├──► SMS
   └──► Push
```

RabbitMQ handles asynchronous processing, while the Strategy Pattern selects the appropriate notification provider.

---

## Retry Mechanism

Failed notification deliveries are retried up to **3 times**.

```text
Attempt 1
   │
   ├── Success → Sent
   └── Failure
         ↓
      Attempt 2
         │
         ├── Success → Sent
         └── Failure
               ↓
            Attempt 3
               │
               ├── Success → Sent
               └── Failure → Failed
```

Each delivery stores its **status, retry count, error message, and sent timestamp**.

---

## API

### Create Notification

```http
POST /api/Notifications
```

```json
{
  "userId": "USER_ID",
  "templateId": 1
}
```

### Get Notification

```http
GET /api/Notifications/{id}
```

---

## Technologies

| Technology | Purpose |
|---|---|
| C# | Programming Language |
| .NET 10 | Application Framework |
| ASP.NET Core | Web API |
| Entity Framework Core | ORM |
| SQL Server | Database |
| RabbitMQ | Message Broker |
| ASP.NET Core Identity | Authentication & User Management |
| FluentValidation | Request Validation |
| Swagger | API Documentation |
| Docker | Containerization |

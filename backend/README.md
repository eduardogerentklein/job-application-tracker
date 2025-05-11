# Job Application Tracker - Backend

The backend for the Job Application Tracker is built with **.NET 9** and provides a RESTful API to manage job applications.

## Features

- **Job Application Management**: Add, update, delete, and retrieve job applications.
- **Status Tracking**: Manage application statuses (e.g., Applied, Interview, Offer, Rejected, Accepted).
- **Swagger Integration**: Interactive API documentation available at `/swagger`.
- **Rate Limiting**: Protects the API from abuse by limiting requests to **30 requests per minute per IP**.
- **SQLite Database**: Lightweight and efficient database for storing application data.
- **Dependency Injection**: Follows the dependency injection pattern for better modularity and testability.
- **Repository Pattern**: Implements the repository pattern for data access abstraction.

## Technologies

- **.NET 9**
- **Entity Framework Core** with SQLite
- **Swagger/OpenAPI** for API documentation
- **Serilog** for logging
- **Rate Limiter** for request throttling

## Architecture

The backend follows a clean architecture with the following layers:

1. **Domain**: Contains core business logic and entities.
2. **Application**: Handles use cases and application logic.
3. **Infrastructure**: Manages data access, database migrations, and external integrations.
4. **Common**: Contains shared/common code and utilities that can be reused across multiple layers.
5. **API**: Exposes RESTful endpoints and handles HTTP requests.

## Dependencies

The backend uses the following dependencies:

- `Microsoft.EntityFrameworkCore.Sqlite`: SQLite database provider.
- `Microsoft.EntityFrameworkCore.Tools`: Tools for managing database migrations.
- `Microsoft.EntityFrameworkCore.Design`: Design-time support for EF Core.
- `AspNetCore.HealthChecks.Sqlite`: Health checks for SQLite.
- `Serilog`: Logging framework.
- `Swashbuckle.AspNetCore`: Swagger/OpenAPI integration.

## Rate Limiting

The backend implements rate limiting to prevent abuse. The configuration is as follows:

- **Limit**: 30 requests per minute per IP.
- **Queue Limit**: 2 requests.
- **Rejection Status Code**: HTTP 429 (Too Many Requests).

This is implemented using the `AddRateLimiter` method in the [`DependencyInjection`](backend/JobApplicationTracker.Api/DependencyInjection.cs) class.

## Database

The backend uses **SQLite** as its database. The connection string is configured in the `appsettings.Development.json` file. Entity Framework Core is used for database access and migrations.

## Repository Pattern

The backend implements the **Repository Pattern** to abstract data access. Examples include:

- [`JobApplicationRepository`](backend/JobApplicationTracker.Infrastructure/Repositories/JobApplicationRepository.cs)
- [`ApplicationStatusRepository`](backend/JobApplicationTracker.Infrastructure/Repositories/ApplicationStatusRepository.cs)

## Dependency Injection

The backend uses **Dependency Injection** to manage dependencies. Services, repositories, and other components are registered in the `DependencyInjection` classes across layers. Examples include:

- [`AddApplication`](backend/JobApplicationTracker.Application/DependencyInjection.cs)
- [`AddInfrastructure`](backend/JobApplicationTracker.Infrastructure/DependencyInjection.cs)
- [`AddSwagger`](backend/JobApplicationTracker.Api/DependencyInjection.cs)

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQLite

### Running the Application

### Visual Studio

1. Clone the repository:

```bash
git clone https://github.com/eduardogerentklein/job-application-tracker.git
```

2. Open the cloned repository in Visual Studio

3. Apply database migrations:

- Migrations are applied once the application is running, using the MigrateAsync() method.

4. Run the application:

- Press the play button on top or hit F5.

5. Access the Swagger UI at http://localhost:32768/swagger.

### vscode

1. Clone the repository:

```bash
git clone https://github.com/eduardogerentklein/job-application-tracker.git
cd job-application-tracker/backend

```

2. Restore dependencies:

- dotnet restore

3. Apply database migrations:

- Migrations are applied once the application is running, using the MigrateAsync() method.

4. Run the application:

- dotnet run --project JobApplicationTracker.Api

5. Access the Swagger UI at http://localhost:32768/swagger.

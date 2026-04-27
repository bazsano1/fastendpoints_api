# AI Agent Instructions for FastEndpoints API

This guide helps AI coding agents understand and contribute to the FastEndpoints API project effectively.

## 📌 Quick Reference

| Task | Command | Location |
|------|---------|----------|
| **Build** | `dotnet build` | `src/FastEndpointApi/` |
| **Run API** | `dotnet run` | `src/FastEndpointApi/` (https://localhost:7089) |
| **Run Tests** | `dotnet test` | Repository root |
| **Restore Deps** | `dotnet restore` | Repository root |
| **Docker Kafka** | `docker-compose up -d` | `kafka/` directory |

## 🎯 Project Overview

**FastEndpoints API** is a lightweight REST API built with [FastEndpoints 8.1.0](https://www.nuget.org/packages/FastEndpoints) on **.NET 10**. It demonstrates minimal boilerplate, clean endpoint architecture, and type-safe request/response handling.

**Tech Stack:**
- Framework: FastEndpoints 8.1.0
- Runtime: .NET 10 SDK+
- Optional Integration: Confluent Kafka 2.14.0
- Testing: Xunit

**Full project details:** See [README.md](README.md)

## 🏗️ Architecture

### Endpoint Pattern
All API endpoints inherit from FastEndpoints base classes:
```csharp
// With typed request/response
public class SongEndpoint : Endpoint<SongRequest, SongResponse>
{
    public override void Configure()
    {
        Post("api/songs");  // Route & HTTP verb
        AllowAnonymous();   // Security
    }

    public override async Task HandleAsync(SongRequest req, CancellationToken ct)
    {
        // Business logic
    }
}

// Without request input
public class HelloWorld : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
    }
}
```

### Request/Response Models (Domain)
Located in `src/FastEndpointApi/Domain/`:
- Request DTOs: Typically nullable properties (optional input)
- Response DTOs: Required properties with data
- Example: `SongRequest.cs` and `SongResponse.cs`

### Application Startup
`Program.cs` is minimal:
1. Create builder with default web host config
2. Register FastEndpoints: `builder.Services.AddFastEndpoints()`
3. Build and run app
4. Automatically discovers endpoints in assembly

## 📂 Project Structure

```
fastendpoints_api/
├── src/FastEndpointApi/           [PRODUCTION CODE]
│   ├── Endpoints/                 # FastEndpoints handlers
│   │   ├── HelloWorld.cs          # GET / endpoint pattern
│   │   ├── SongEndpoint.cs        # POST /api/songs with validation
│   │   └── KafkaEndpoint.cs       # POST /api/kafka/send
│   ├── Domain/                    # Request/response DTOs
│   │   ├── SongRequest.cs
│   │   ├── SongResponse.cs
│   │   └── SendMessageRequest.cs
│   ├── Kafka/                     # External integration
│   │   └── KafkaProducer.cs       # Kafka producer (hardcoded localhost:9092)
│   ├── Program.cs                 # App startup & FastEndpoints config
│   ├── FastEndpointApi.csproj     # Project file (nullable=enable, implicit usings)
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Properties/launchSettings.json
│
├── NetLearnSamples/               [NOT API - Learning sandbox]
│   ├── Book.cs, Customer.cs
│   └── Program.cs
│
├── NetLearnSamples.Tests/         [NOT API TESTS - Learning tests]
│   ├── BookStoreTests.cs
│   ├── UpdateCustomerTests.cs
│   └── NetLearnSamples.Tests.csproj
│
├── kafka/
│   └── docker-compose.yaml        # Optional Kafka infrastructure
│
├── README.md                       # Full documentation
├── FastEndpointApi.slnx           # Solution file
└── AGENTS.md                      # This file
```

## ⚙️ Development Conventions

### Naming & Organization
- **Endpoints:** Class name matches functionality (e.g., `SongEndpoint`, `KafkaEndpoint`)
- **Routes:** RESTful conventions (`POST /api/songs`, `GET /`)
- **DTOs:** Separate request/response classes in `Domain/`

### Async/Await Pattern
- All handlers use `async Task HandleAsync()` with `CancellationToken ct`
- Always pass `cancellation: ct` to response methods
- Example: `await Send.OkAsync(response, cancellation: ct)`

### Response Handling
FastEndpoints provides built-in response methods:
- `await Send.OkAsync(data, cancellation: ct)` → HTTP 200
- `await Send.NotFoundAsync(ct)` → HTTP 404
- `await Send.BadRequestAsync(ct)` → HTTP 400

### Nullable Reference Types
- **Enabled** in `.csproj` (`<Nullable>enable</Nullable>`)
- Request DTOs: Use nullable properties for optional input
- Response DTOs: Use required properties (or nullable with `?`)

## 🔧 Common Tasks

### Add a New Endpoint
1. Create `src/FastEndpointApi/Endpoints/MyEndpoint.cs`
2. Inherit from `Endpoint<TRequest, TResponse>` or `EndpointWithoutRequest`
3. Override `Configure()` → set HTTP verb + route
4. Override `HandleAsync()` → implement logic
5. Endpoints are auto-discovered; no registration needed

### Add a Request/Response DTO
1. Create file in `src/FastEndpointApi/Domain/`
2. Use nullable properties for optional input (requests)
3. Use required properties for response data

### Run Tests
```powershell
# All tests
dotnet test

# Specific project
dotnet test NetLearnSamples.Tests/NetLearnSamples.Tests.csproj

# Specific test
dotnet test --filter "FullyQualifiedName~BookStoreTests"
```

### Enable Kafka (Optional)
1. Ensure Docker Desktop is running
2. `cd kafka && docker-compose up -d`
3. Kafka broker will be available at `localhost:9092`
4. API endpoint: `POST /api/kafka/send` accepts `SendMessageRequest`

## ⚠️ Important Gotchas & Limitations

### Kafka Configuration
- **Hardcoded** to `localhost:9092` in `KafkaProducer.cs`
- Not configurable via `appsettings.json`
- No dependency injection for producer
- Kafka producer created inline, not singleton

### Architecture Limitations
- **No database layer** (responses are hardcoded/in-memory)
- **No input validation** beyond manual checks in handlers
- **No API-specific tests** (only learning sample tests in `NetLearnSamples.Tests/`)
- **No authentication/authorization** (endpoints use `AllowAnonymous()`)

### Development Environment
- Requires **.NET 10 SDK** or later
- Kafka integration is **optional** (not required for basic API)
- Tests use **Xunit** framework

## 📚 Useful Resources

- [FastEndpoints Documentation](https://fast-endpoints.com/) - Official docs
- [Confluent Kafka C# Client](https://github.com/confluentinc/confluent-kafka-dotnet) - Kafka integration
- [README.md](README.md) - Full project documentation
- [.NET 10 Release Notes](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10) - Framework features

## 🤖 AI Agent Workflow

When working on this project:

1. **Understand scope**: Identify if changes affect API code (`src/FastEndpointApi/`) vs. learning code (`NetLearnSamples/`)
2. **Check patterns**: New endpoints should follow `SongEndpoint.cs` and `HelloWorld.cs` patterns
3. **Validate async**: Always use `async Task` and pass `CancellationToken ct` to responses
4. **Test thoroughly**: Use `dotnet test` before committing
5. **Consider gotchas**: Kafka is hardcoded; no DI for producer; no database layer
6. **Link documentation**: Refer to [README.md](README.md) for detailed API endpoints and getting started guides

---

**Last Updated:** 2026-04-27  
**Framework:** FastEndpoints 8.1.0 | .NET 10  
**Maintainer:** AI Agent Guidelines

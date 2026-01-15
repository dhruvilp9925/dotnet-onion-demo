# OnionDemo Codebase AI Instructions

## Architecture Overview
This is a **Clean Architecture (Onion Architecture)** .NET project with layered separation using .NET 10.0.

**Layer Stack (outer → inner):**
- **API Layer** (`OnionDemo.API`): ASP.NET Core REST controllers - handles HTTP requests, dependency injection setup
- **Application Layer** (`OnionDemo.Application`): Service interfaces - defines contracts that bridge API and infrastructure
- **Infrastructure Layer** (`OnionDemo.Infrastructure`): Service implementations - actual business logic and data handling
- **Domain Layer** (`OnionDemo.Domain`): Entities and domain models - core business objects, agnostic to infrastructure

**Critical Rule**: Dependency flow is unidirectional inward. API depends on Application and Infrastructure, but Domain must never depend on other layers.

## Key Patterns

### Dependency Injection & Service Registration
All services use constructor injection with interface-based contracts:
- Define interfaces in `OnionDemo.Application.Interfaces` (e.g., `IUserService`)
- Implement in `OnionDemo.Infrastructure.Services` (e.g., `UserService`)
- Register in `Program.cs` using `builder.Services.AddScoped<IInterface, Implementation>()`
- Example: Controllers in `UsersController.cs` inject `IUserService` via constructor

### Entity Design
- Entities live in `OnionDemo.Domain.Entities` and inherit from `BaseEntity` (provides `Guid Id`)
- No navigation properties or EF dependencies; domain entities are simple POCOs
- Example: `User` has only `Id` and `Name`

### Controller Pattern
- Controllers inherit from `ControllerBase`
- Route attributes use `[Route("api/users")]` for RESTful paths
- Inject interfaces, never concrete implementations
- Return `IActionResult` with `Ok()` wrapper
- See `UsersController.cs` for pattern

## Development Workflow

### Building & Running
```bash
dotnet build                    # Builds all projects
dotnet run --project OnionDemo.API  # Runs API on https://localhost:5001
```

### Testing API
- Swagger UI auto-generates at `/swagger` when running locally
- Use `OnionDemo.API.http` file for REST client requests
- API endpoint example: `GET /api/users` or `POST /api/users?name=John`

### Adding New Features
1. Create entity in `OnionDemo.Domain.Entities`
2. Create interface in `OnionDemo.Application.Interfaces`
3. Implement in `OnionDemo.Infrastructure.Services`
4. Register in `Program.cs` with `AddScoped<>`
5. Create controller in `OnionDemo.API.Controllers`

## Project Specifics

- **Target Framework**: .NET 10.0 (latest, requires recent SDK)
- **Nullable**: Enabled - all reference types are non-nullable by default; use `?` for optional
- **Implicit Usings**: Enabled - no need for explicit `using System` statements
- **Current Data Storage**: In-memory static list (`UserService._users`); no database yet

## Conventions
- Namespaces follow folder structure: `OnionDemo.{Layer}.{Folder}`
- File names match class names (PascalCase)
- Interfaces prefixed with `I`
- Methods use PascalCase; parameters use camelCase
- Empty `Class1.cs` files in layers are placeholders - delete as needed

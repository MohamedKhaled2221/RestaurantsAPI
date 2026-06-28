# 🍽️ Restaurant API

A production-ready RESTful API for restaurant management, built with **ASP.NET Core 8** following **Clean Architecture** and **CQRS** principles. Deployed on **Azure** with automated CI/CD pipelines.

---

## 🚀 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 |
| Architecture | Clean Architecture + CQRS |
| ORM | Entity Framework Core |
| Database | Azure SQL / MS SQL Server |
| Messaging | MediatR |
| Auth | ASP.NET Identity + JWT |
| Validation | FluentValidation |
| Logging | Serilog |
| Documentation | Swagger / OpenAPI |
| Testing | xUnit + Integration Tests |
| Cloud | Azure App Service + Azure SQL |
| CI/CD | GitHub Actions |

---

## ✨ Features

- 🔐 **Authentication & Authorization** — JWT-based auth with ASP.NET Identity, roles, and custom claims
- 🍕 **Restaurant & Dish Management** — Full CRUD for restaurants and their nested dish resources
- 📄 **Pagination & Sorting** — Optimized queries for large datasets
- 🗂️ **File Handling** — Upload and serve files through the API
- 📋 **Automated Documentation** — Swagger UI for easy API exploration
- 🛡️ **Global Exception Handling** — Consistent error responses across all endpoints
- 📊 **Structured Logging** — Application events and errors logged via Serilog
- ✅ **Automated Testing** — Unit and integration test coverage
- ☁️ **Azure Deployment** — Hosted on Azure App Service with Azure SQL database
- 🔄 **CI/CD Pipeline** — Automated build, test, and deployment via GitHub Actions

---

## 🏗️ Architecture

The project follows **Clean Architecture** with a clear separation of concerns across four layers:

```
RestaurantAPI/
│
├── RestaurantAPI.Domain/          # Entities, interfaces, domain logic
├── RestaurantAPI.Application/     # CQRS commands/queries, DTOs, validators, MediatR handlers
├── RestaurantAPI.Infrastructure/  # EF Core, database, external services, identity
└── RestaurantAPI.API/             # Controllers, middleware, Swagger, program entry point
```

### CQRS Flow

```
Request → Controller → MediatR → Command/Query Handler → Repository → Database
```

---

## 📦 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or Azure SQL
- [Git](https://git-scm.com/)

### Installation

```bash
# Clone the repository
git clone https://github.com/your-username/restaurant-api.git
cd restaurant-api
```

### Configuration

Update `appsettings.json` in the API project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string here"
  },
  "JwtSettings": {
    "Key": "your-secret-key",
    "Issuer": "RestaurantAPI",
    "Audience": "RestaurantAPIUsers"
  }
}
```

### Run the API

```bash
cd RestaurantAPI.API
dotnet ef database update
dotnet run
```

The API will be available at `https://localhost:7000`  
Swagger UI: `https://localhost:7000/swagger`

---

## 🔌 API Endpoints

### 🔐 Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/identity/register` | Register a new user |
| POST | `/api/identity/login` | Login and receive JWT token |

### 🍽️ Restaurants
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/restaurants` | Get all restaurants (paginated) |
| GET | `/api/restaurants/{id}` | Get restaurant by ID |
| POST | `/api/restaurants` | Create a new restaurant |
| PUT | `/api/restaurants/{id}` | Update a restaurant |
| DELETE | `/api/restaurants/{id}` | Delete a restaurant |

### 🍕 Dishes (Sub-resource)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/restaurants/{id}/dishes` | Get all dishes for a restaurant |
| GET | `/api/restaurants/{id}/dishes/{dishId}` | Get a specific dish |
| POST | `/api/restaurants/{id}/dishes` | Add a dish to a restaurant |
| DELETE | `/api/restaurants/{id}/dishes` | Remove all dishes from a restaurant |

---

## 🧪 Running Tests

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"
```

---

## ☁️ Azure Deployment

The app is deployed on **Azure App Service** with **Azure SQL Database**.

### CI/CD Pipeline

Every push to `main` triggers the GitHub Actions pipeline:

1. ✅ Build & Restore
2. 🧪 Run Tests
3. 📦 Publish Artifact
4. 🚀 Deploy to Azure App Service

The pipeline configuration is located at `.github/workflows/deploy.yml`.

---

## 📁 Project Structure

```
RestaurantAPI/
│
├── .github/
│   └── workflows/
│       └── deploy.yml              # CI/CD pipeline
│
├── RestaurantAPI.Domain/
│   ├── Entities/                   # Restaurant, Dish, User
│   └── Interfaces/                 # Repository contracts
│
├── RestaurantAPI.Application/
│   ├── Restaurants/
│   │   ├── Commands/               # Create, Update, Delete commands
│   │   └── Queries/                # GetAll, GetById queries
│   ├── Dishes/
│   ├── DTOs/                       # Data Transfer Objects
│   └── Validators/                 # FluentValidation rules
│
├── RestaurantAPI.Infrastructure/
│   ├── Persistence/                # EF Core DbContext & migrations
│   ├── Repositories/               # Repository implementations
│   └── Seeders/                    # Database seeding
│
└── RestaurantAPI.API/
    ├── Controllers/                # API endpoints
    ├── Middlewares/                # Global exception handler
    └── Program.cs                  # App configuration & DI
```

---

## 🔒 Authorization Roles

| Role | Permissions |
|------|-------------|
| `Admin` | Full access — create, update, delete restaurants & dishes |
| `User` | Read-only access to restaurants and dishes |
| `Owner` | Manage only their own restaurants |



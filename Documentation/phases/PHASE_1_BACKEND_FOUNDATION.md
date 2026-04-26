# Phase 1 — Backend Foundation

---

## 1. Phase Objective

The goal of this phase was to build a clean, structured, and production-ready backend foundation using ASP.NET Core.

This phase focuses on:

- setting up ASP.NET Core Web API
- configuring Program.cs and middleware pipeline
- integrating EF Core with SQL Server
- implementing Dependency Injection (DI)
- understanding request lifecycle
- creating a consistent API response structure
- adding logging and error handling

---

## 2. What Was Built

### Backend

- ASP.NET Core Web API setup
- Program.cs configured
- EF Core + SQL Server integration
- AppDbContext created
- Dependency Injection configured
- Health endpoint created
- Swagger (API testing UI) added
- Migrations created and applied
- Global exception middleware implemented
- Request logging middleware implemented
- Standard API response structure created

### Frontend

- Angular kept ready (no major work in this phase)

---

## 3. Backend Work Done

- Project references configured
- EF Core packages installed
- dotnet-ef tool installed
- AppDbContext created
- Connection string configured in appsettings.json
- Dependency Injection setup using extension method
- HealthController created (`GET /api/health`)
- Swagger configured and tested
- Migration created (`InitialCreate`)
- Database created (`StockFlowDb`)
- Tables created:
  - Products
  - Users
- Decimal precision configured for `PurchasePrice`
- ApiResponse<T> created for standard responses
- AppException created for custom error handling
- ExceptionMiddleware implemented
- RequestLoggingMiddleware implemented
- Middleware pipeline configured

---

## 4. Frontend Work Done

- Angular application prepared for API integration
- No UI features implemented in this phase

---

## 5. What I Learned

- how backend systems are structured
- how request flows inside ASP.NET Core
- how Dependency Injection works in real applications
- how EF Core connects code to database
- how middleware works and executes in sequence
- importance of logging and error handling
- how to design consistent and predictable API responses

---

## 6. System Design and Architecture Concepts Covered

---

### 🔹 6.1 Layered Architecture

The system is divided into logical layers:

```

API → Application
API → Infrastructure

Application → Domain

Infrastructure → Application
Infrastructure → Domain

Domain → (independent)

```

Meaning:

- API → handles HTTP requests and responses
- Application → contains business logic
- Domain → contains core entities (Product, User)
- Infrastructure → handles database and external systems

Important rule:

```

Outer layer can depend on inner layer
Inner layer must NEVER depend on outer layer

```

---

### 🔹 6.2 Request Lifecycle

When a request comes to the system:

```

Client (Browser / Postman)
↓
Middleware (Logging / Exception)
↓
Controller
↓
Service (future)
↓
DbContext
↓
Database
↓
Response (JSON)

```

Step-by-step:

1. Request comes to the server
2. Middleware processes the request first
3. Controller handles the request
4. Data is processed
5. Response is returned to the client

---

### 🔹 6.3 Middleware Pipeline

Middleware is a component that runs on every HTTP request and response.

It works like a pipeline, where each middleware runs one after another.

```

Request
↓
ExceptionMiddleware
↓
RequestLoggingMiddleware
↓
Controller
↓
Response

````

#### How it works:

1. A request comes from the client (browser or Postman)
2. The request enters the middleware pipeline
3. Each middleware processes the request
4. The request reaches the controller
5. The response is generated and sent back

#### What each middleware does:

- **ExceptionMiddleware**  
  Handles errors globally.  
  If any error occurs anywhere in the system, it catches the error and returns a clean JSON response.

- **RequestLoggingMiddleware**  
  Logs request details such as:
  - HTTP method (GET, POST, etc.)
  - URL path
  - status code
  - execution time

- **Controller**  
  Handles the request and runs business logic.

#### Important concept:

Middleware runs in a sequence (pipeline).

Each middleware can:

- process the request
- pass it to the next middleware using `_next(context)`
- or stop the request completely

If `_next(context)` is not called, the request will stop there.

This allows cross-cutting concerns (like logging and error handling) to be handled in one place instead of repeating code in multiple controllers.

---

### 🔹 6.4 Dependency Injection (DI)

Dependency Injection is a design pattern used to provide objects automatically instead of creating them manually.

Means: Instead of creating objects manually using `new`, ASP.NET Core automatically provides required objects.

Instead of writing:

```csharp
var context = new AppDbContext();
````

ASP.NET Core provides the object automatically.

#### Example:

```csharp
public ProductsController(AppDbContext context)
{
    _context = context;
}
```

ASP.NET Core injects `AppDbContext` when the controller is created.

#### How it works:

1. The service is registered:

```csharp
services.AddDbContext<AppDbContext>();
```

2. ASP.NET Core stores it in a container (DI Container)

3. When needed, it automatically provides the object

#### Benefits:

* cleaner code
* easier testing
* reduced coupling between components
* better maintainability

---

### 🔹 6.5 EF Core + Database Flow

Entity Framework Core (EF Core) is an ORM (Object Relational Mapper).

It connects C# code with a relational database like SQL Server.

Each C# entity class is mapped to a database table by EF Core.

#### Flow:

```
Entity (Product.cs)
↓
AppDbContext
↓
Migration
↓
SQL Server
↓
Table Created
```

#### Step-by-step:

1. **Entity (Domain Layer)**
   A C# class represents data.

```csharp
public class Product
{
    public string Name { get; set; }
}
```

2. **DbContext (Infrastructure Layer)**
   Defines which entities become database tables.

```csharp
public DbSet<Product> Products { get; set; }
```

3. **Migration**

```bash
dotnet ef migrations add InitialCreate
```

EF Core generates SQL scripts based on your entities.

4. **Database Update**

```bash
dotnet ef database update
```

The database is created and tables are applied.

#### Important concept:

Code First Approach

You write C# code → EF Core creates database structure automatically.

#### Example usage:

```csharp
_context.Products.Add(product);
await _context.SaveChangesAsync();
```

EF Core internally converts this into SQL and saves data in the database.

#### Why EF Core is useful:

* no need to write raw SQL
* supports migrations
* tracks database changes
* simplifies CRUD operations

---

### 🔹 6.6 Logging Concept

```
Incoming Request → Log
Processing → Measure time
Completed Request → Log
```

Why logging is important:

* helps debugging
* tracks performance
* useful in production monitoring

---

### 🔹 6.7 Error Handling Strategy

Before:

* random errors
* inconsistent responses
* unclear debugging

Now:

* clean JSON error response
* controlled status codes
* centralized error handling

Example:

```json
{
  "success": false,
  "message": "Product not found",
  "data": null,
  "errors": null
}
```

---

### 🔹 6.8 API Response Standardization

All API responses follow a consistent structure using `ApiResponse<T>`.

Example:

```json
{
  "success": true,
  "message": "API is healthy",
  "data": { ... },
  "errors": null
}
```

Why this matters:

* frontend always knows where data is
* error handling becomes predictable
* API design becomes consistent

---

## 7. SOLID / Design Principles Applied

---

### 🔹 Single Responsibility Principle (SRP)

Each class has a single responsibility:

* Controller → handles request/response
* DbContext → handles database interaction
* Middleware → handles cross-cutting concerns

---

### 🔹 Dependency Inversion Principle (DIP)

High-level modules do not depend on low-level implementations.

* API does NOT create DbContext manually
* DI provides required dependencies

---

### 🔹 KISS (Keep It Simple, Stupid)

* avoided unnecessary complexity
* used simple layered architecture
* no over-engineering

---

### 🔹 DRY (Don’t Repeat Yourself)

* error handling centralized in middleware
* no repeated try-catch blocks in controllers

---

### 🔹 Separation of Concerns

Each layer has a clear responsibility:

* API → request/response
* Application → business logic
* Domain → core data
* Infrastructure → database

---

## 8. Key Engineering Decisions

---

### Use Layered Architecture

**Why:**

- separates responsibilities across layers (API, Application, Domain, Infrastructure)
- improves maintainability and readability
- allows independent changes without breaking the entire system
- makes the system scalable as features grow

---

### Use EF Core (Code First Approach)

**Why:**

- reduces need to write raw SQL
- supports migrations for version-controlled database changes
- integrates well with ASP.NET Core
- allows rapid development using C# models

---

### Use Dependency Injection (Built-in DI Container)

**Why:**

- avoids manual object creation
- reduces tight coupling between components
- improves testability (easy to mock dependencies)
- follows modern backend development practices

---

### Use Middleware for Error Handling

**Why:**

- centralizes error handling in one place
- avoids repeating try-catch in controllers
- ensures consistent error response format
- improves maintainability and debugging

---

### Use Standard API Response Structure (ApiResponse<T>)

**Why:**

- enforces a consistent API contract
- simplifies frontend integration
- makes success and error responses predictable
- improves debugging and logging

---

### Use Request Logging Middleware

**Why:**

- tracks incoming and outgoing requests
- helps measure API performance (execution time)
- useful for debugging and monitoring
- important for production observability

---

## 9. Alternatives Considered

---

### Single Project Backend

**Pros:**

- simple to set up
- fewer files and folders

**Cons:**

- difficult to maintain as project grows
- mixes responsibilities (controller, logic, database)
- not scalable for real-world applications

Not chosen because this project is designed for learning clean architecture and scalability.

---

### Manual SQL (Without ORM)

**Pros:**

- full control over queries
- potentially better performance for complex queries

**Cons:**

- more boilerplate code
- harder to maintain
- no automatic migrations
- increases development time

Not chosen because EF Core provides a good balance between productivity and control.

---

### No Middleware-Based Error Handling

**Pros:**

- simple implementation (local try-catch)

**Cons:**

- repeated code across controllers
- inconsistent error responses
- difficult to maintain and debug

Not chosen because centralized middleware provides cleaner and more scalable error handling.

---

### No Logging Middleware

**Pros:**

- less initial setup

**Cons:**

- no visibility into request flow
- difficult debugging
- no performance tracking

Not chosen because logging is essential for real-world backend systems.

---

## 10. Why This Phase Matters

This phase builds the core foundation of the backend system.

Without this:

* messy codebase
* difficult debugging
* poor scalability

With this:

* clean architecture
* structured development
* production-ready base

---

## 11. Challenges Faced

* understanding Dependency Injection
* understanding middleware flow
* EF Core configuration
* migration warning (decimal precision)
* managing multiple projects and references

---

## 12. How I Solved Them

* followed layered architecture strictly
* used step-by-step approach
* fixed decimal precision in DbContext
* tested endpoints using Swagger
* verified using build and run

---

## 13. Industry-Standard Practices Followed

* clean architecture
* Dependency Injection
* centralized error handling
* structured logging
* API documentation using Swagger
* migration-based database management

---

## 14. Topics Covered After Completing This Phase

After this phase, I covered:

* ASP.NET Core Web API fundamentals
* middleware pipeline
* Dependency Injection
* EF Core setup and migrations
* SQL Server integration
* logging and error handling
* API design fundamentals

---
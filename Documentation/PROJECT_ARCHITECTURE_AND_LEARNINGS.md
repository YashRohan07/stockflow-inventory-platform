# Project Architecture and Learnings

---

## 1. Project Overview

StockFlow is a full-stack Inventory & Product Management Platform built using ASP.NET Core and Angular.

It is designed as a learning-first, industry-aligned project focusing on backend architecture, system design, and scalable development practices.

---

### Why This Project Was Built

- to learn ASP.NET Core and Angular
- to understand real-world backend architecture
- to improve system design skills

---

### Problem It Solves

- manage product records
- manage inventory and stock data
- provide role-based access (planned)
- support reporting (planned)
- ensure structured and scalable data handling

---

## 2. Technology Stack

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication (planned for future phase)

### Frontend

- Angular
- TypeScript

---

## 3. High-Level Architecture

### Backend

The backend follows a layered architecture.

Each layer has a specific responsibility to keep the system clean and maintainable.

- API Layer → handles HTTP requests and responses
- Application Layer → contains business logic
- Domain Layer → defines core entities (Product, User)
- Infrastructure Layer → handles database and external services

---

### Frontend

The frontend is built using Angular and will follow a feature-based modular structure for scalability and maintainability.

---

## 4. Backend Request Flow (Phase 1)

The backend uses a middleware-based request pipeline.

### Request Flow

```

Client (Browser / Postman)
↓
ExceptionMiddleware
↓
RequestLoggingMiddleware
↓
Controller
↓
DbContext
↓
Database
↓
Response (JSON)

````

### Explanation

1. The client sends a request
2. Middleware processes the request first
3. Controller handles the request
4. Data is processed using DbContext
5. Database returns data
6. Response is sent back as JSON

---

### Key Components

- **ExceptionMiddleware**  
  Handles all errors in one place and returns a clean response

- **RequestLoggingMiddleware**  
  Logs request details like method, path, status code, and time

- **Controller**  
  Handles API endpoints and request logic

- **AppDbContext**  
  Connects the application with the database using EF Core

---

## 5. Current Phase

Phase 1 focuses on building a strong backend foundation.

Completed:

- ASP.NET Core Web API setup
- Program.cs configuration
- Middleware pipeline implementation
- Dependency Injection setup
- EF Core + SQL Server integration
- AppDbContext creation
- database migrations
- Health endpoint
- Swagger integration
- global exception handling
- request logging middleware
- standard API response structure

---

## 6. Engineering Decisions

### Why ASP.NET Core + Angular

- strong backend support
- scalable frontend structure
- suitable for full-stack applications

---

### Why Layered Architecture

- separates responsibilities
- improves maintainability
- supports scalability

---

### Why Dependency Injection

- avoids manual object creation
- reduces tight coupling
- improves testability

---

### Why Middleware

- centralizes cross-cutting concerns
- avoids repeated code
- improves maintainability

---

### Why EF Core

- reduces need for manual SQL
- supports migrations
- integrates well with ASP.NET Core

---

## 7. Key Architectural Principles

- separation of concerns
- layered architecture
- dependency injection
- middleware-based request handling
- modular structure
- domain-first design

---

## 8. API and Response Design

To maintain consistency, all API responses follow a standard structure.

### Success Response

```json
{
  "success": true,
  "message": "Operation successful",
  "data": {},
  "errors": null
}
````

---

### Error Response

```json
{
  "success": false,
  "message": "Something went wrong",
  "data": null,
  "errors": null
}
```

---

### Why This Approach

* frontend always knows where data is
* responses are predictable
* easier debugging
* cleaner API design

---

## 9. What I Learned

* how backend systems are structured
* how middleware pipeline works
* how Dependency Injection works
* how EF Core connects code to database
* how logging and error handling improve debugging
* how to design clean and consistent APIs

---

## 10. Why This Architecture Matters

This architecture ensures:

* clean and organized code
* easy debugging and maintenance
* scalable system design
* reusable backend structure for future projects

---

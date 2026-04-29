# Project Architecture and Learnings

---

## 1. Project Overview

### 1.1 What This Project Is

StockFlow is a full-stack Inventory & Product Management Platform built using ASP.NET Core and Angular.

It is designed as a learning-focused but industry-standard project that demonstrates backend architecture, frontend structure, authentication and system design thinking.

---

### 1.2 Why This Project Was Built

* to learn backend and frontend together
* to understand real-world system architecture
* to practice clean code and SOLID principles
* to build a production-style full-stack application

---

### 1.3 Problem It Solves

* manage product records
* manage inventory and stock
* secure system using authentication
* control access using roles (Admin / Member)
* provide structured and scalable system design

---

## 2. Technology Stack

### 2.1 Backend

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Middleware (Logging + Exception Handling)

---

### 2.2 Frontend

* Angular 
* TypeScript
* Angular Router
* HttpClient
* Reactive Forms

---

## 3. High-Level Architecture

---

### 3.1 Backend Architecture

```text
API Layer → Application Layer → Domain Layer
        ↘ Infrastructure Layer (Database, JWT, Hashing)

Controller → Service → Repository → Database
```

* Controller → handles HTTP requests
* Service → contains business logic (validation, rules, mapping)
* Repository → handles database operations
* Domain → core entities
* Infrastructure → database and external services

---

### 3.2 Frontend Architecture

```text
src/app
│
├── core
│   ├── services
│   ├── guards
│   └── interceptors
│
├── shared
│   ├── models
│   └── utils
│
└── features
    ├── products
    ├── auth
    └── reports
```

* core → global logic
* shared → reusable code
* features → business modules

---

## 4. Full Stack Request Flow

```text
User
↓
Angular Component
↓
ProductService / AuthService
↓
HttpClient
↓
ASP.NET Core API
↓
Middleware (Logging + Exception)
↓
Controller
↓
Service
↓
Repository / DbContext
↓
Database
↓
Response
↓
Frontend UI Update
```

---

## 4.1 CRUD Flow (Product Module)

```text
User Action (Create / Update / Delete)
↓
Angular UI (Form / Button)
↓
ProductService (Frontend)
↓
HTTP Request
↓
ASP.NET Controller
↓
ProductService (Backend)
↓
ProductRepository
↓
Database (SQL Server)
↓
Response
↓
Frontend UI Update
```

This flow represents how full-stack CRUD operations work in the system.

---

## 4.2 System Behavior and Data Integrity

The system ensures that:

* SKU is unique across all products
* Quantity and price cannot be negative
* Invalid data is blocked at validation layer before reaching database
* Database constraints provide an additional safety layer

### Failure Handling

* Invalid input → blocked by validation
* Unauthorized access → blocked by JWT + guards
* Server errors → handled by global exception middleware

This ensures predictable and safe system behavior.

---

## 5. Authentication Flow (JWT)

```text
User enters email + password
↓
Frontend sends login request
↓
Backend validates credentials
↓
JWT token generated
↓
Token returned to frontend
↓
Stored in localStorage
↓
Interceptor attaches token
↓
Backend validates token
↓
Access granted or denied
```

---

## 6. DTO-Based API Design

DTO (Data Transfer Object) is used to define how data moves between frontend and backend.

```text
Frontend → CreateProductDto → Backend
Backend → ProductResponseDto → Frontend
```

Benefits:

* prevents exposing internal entity structure
* ensures clean API contract
* separates input and output models
* improves security and maintainability

---

## 7. Phase-by-Phase Summary

---

### Phase 0 — Foundation

* project structure setup
* layered architecture design
* frontend setup
* domain modeling

---

### Phase 1 — Backend Foundation

* ASP.NET Core setup
* EF Core integration
* SQL Server database
* middleware pipeline
* logging and exception handling
* API response structure

---

### Phase 2 — Frontend Foundation

* Angular setup
* routing system
* feature-based architecture
* API service
* environment configuration
* frontend-backend integration
* strong typing

---

### Phase 3 — Authentication & Authorization

* JWT authentication
* login system
* password hashing
* role-based access control
* authorization policies
* Swagger Bearer auth
* frontend login integration
* token storage
* route guards
* interceptor
* logout flow
* token expiry handling

---

### Phase 4 — Product and Inventory Management (CRUD)

* product CRUD APIs implemented (Create, Read, Update, Delete)
* DTO-based API design introduced
* service and repository layers used
* validation implemented for safe data handling
* SKU used as unique business identifier
* frontend product management UI built (list, create, edit, delete)
* Angular reactive forms used for validation
* full frontend-backend integration completed
* products page used as main working screen after login

---

## 8. Key Engineering Decisions

---

### Use Layered Architecture

* clean separation of responsibilities
* scalable structure

---

### Use JWT Authentication

* stateless
* scalable
* suitable for SPA

---

### Use Service + Repository Pattern

* business logic separated
* easier testing

---

### Use DTO-Based API Design

* clean separation between internal data and API contract
* prevents direct exposure of entities
* improves flexibility and maintainability

---

### Use Validation Layer

* prevents invalid data before reaching database
* improves user experience
* ensures data integrity

---

### Use Angular Feature-Based Structure

* modular frontend
* scalable

---

### Use Interceptors

* automatic token attachment
* cleaner API calls

---

### Use Guards

* frontend route protection
* better UX

---

## 8.1 Design Trade-offs

### Not using full Clean Architecture

* simpler layered approach used
* easier to implement and understand
* avoids over-engineering

---

### No caching implemented

* simpler system
* easier debugging
* acceptable for small-scale usage

---

### No concurrency handling (yet)

* acceptable for low user count
* will be improved later

---

## 9. Cross-Cutting Engineering Practices

* separation of concerns
* clean code principles
* validation strategy
* structured error handling
* logging
* security (JWT)
* consistent API design
* debugging through structured logs
* request tracing using logging middleware

---

## 10. Key Challenges and Solutions

---

### Challenge: Swagger token not working

**Solution:** fixed Bearer configuration

---

### Challenge: 401 vs 403 confusion

**Solution:** tested with Admin and Member roles

---

### Challenge: frontend-backend integration

**Solution:** used AuthService + models

---

### Challenge: route security

**Solution:** implemented guards

---

### Challenge: Product list not loading on first click

**Solution:** fixed Angular change detection using ChangeDetectorRef

---

### Challenge: Redirect issue after create/update/delete

**Solution:** improved routing and navigation flow

---

## 11. What I Learned

---

### Backend

* JWT authentication
* middleware pipeline
* EF Core
* dependency injection

---

### Frontend

* Angular architecture
* routing
* interceptors
* guards
* reactive forms

---

### Full Stack

* how CRUD systems work end-to-end
* how frontend and backend communicate
* importance of DTO and validation
* separation of business logic and data access

---

### System Design

* request flow
* stateless systems
* role-based access
* client-server interaction

---

## 12. Mistakes and Improvements

* initially no route protection
* login page accessible after login
* token expiry not handled
* dashboard unnecessarily separated

**Improved by:**

* adding guards
* adding login guard
* redirecting to products page
* simplifying navigation

---

## 13. Future Improvements

* search (SKU, name)
* filtering (date range, quantity)
* sorting (price, stock)
* pagination
* refresh token system
* role-based UI
* audit logging
* caching
* CI/CD pipeline
* deployment

---

## 14. System Readiness Level

Current system is:

* suitable for small-scale production use
* supports multiple authenticated users
* maintains data integrity
* follows clean architecture principles

Limitations:

* no pagination yet
* no caching
* no advanced reporting

These will be addressed in future phases.

---

## 15. Final Reflection

This project helped transition from simple coding to structured engineering.

It improved:

* system thinking
* architecture understanding
* real-world backend + frontend integration
* security implementation
* full-stack development skills


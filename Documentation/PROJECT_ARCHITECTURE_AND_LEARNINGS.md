# Project Architecture and Learnings

---

## 1. Project Overview

### 1.1 What This Project Is

StockFlow is a full-stack Inventory & Product Management Platform built using ASP.NET Core and Angular.

It is designed as a learning-focused but industry-standard project that demonstrates backend architecture, frontend structure, authentication, and system design thinking.

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

* Angular (Standalone)
* TypeScript
* Angular Router
* HttpClient
* Signals (state handling)

---

## 3. High-Level Architecture

---

### 3.1 Backend Architecture

```text
API Layer → Application Layer → Domain Layer
        ↘ Infrastructure Layer (Database, JWT, Hashing)
```

* API → handles HTTP requests
* Application → business logic
* Domain → core entities
* Infrastructure → database, authentication

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
    ├── dashboard
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
AuthService / ApiService
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

## 6. Phase-by-Phase Summary

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

## 7. Key Engineering Decisions

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

## 8. Alternatives Considered

---

### Layered vs Clean Architecture

* Clean Architecture → more complex
* Layered → simpler and practical

**Chosen:** Layered Architecture

---

### JWT vs Session-Based Auth

* Session → stateful, less scalable
* JWT → stateless, scalable

**Chosen:** JWT

---

### SQL vs NoSQL

* SQL → structured data, relationships
* NoSQL → flexible but not needed

**Chosen:** SQL Server

---

## 9. Cross-Cutting Engineering Practices

* separation of concerns
* clean code principles
* validation strategy
* structured error handling
* logging
* security (JWT)
* consistent API design

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

**Improved by:**

* adding guards
* adding login guard
* adding expiry check

---

## 13. Future Improvements

* refresh token system
* role-based UI
* audit logging
* caching
* CI/CD pipeline
* deployment

---

## 14. Final Reflection

This project helped transition from simple coding to structured engineering.

It improved:

* system thinking
* architecture understanding
* real-world backend + frontend integration
* security implementation

---


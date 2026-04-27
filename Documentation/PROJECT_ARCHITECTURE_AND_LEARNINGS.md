# Project Architecture and Learnings

---

## 1. Project Overview

StockFlow is a full-stack Inventory & Product Management Platform built using ASP.NET Core and Angular.

It is designed as a learning-first, industry-aligned project focusing on backend architecture, frontend structure, system design, and scalable development practices.

---

### Why This Project Was Built

* to learn ASP.NET Core and Angular together
* to understand real-world full-stack architecture
* to improve system design thinking
* to build a production-ready project structure

---

### Problem It Solves

* manage product records
* manage inventory and stock data
* provide role-based access (planned)
* support reporting (planned)
* ensure clean, structured, and scalable data handling

---

## 2. Technology Stack

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication (planned)

---

### Frontend

* Angular (Standalone)
* TypeScript
* Angular Router
* HttpClient

---

## 3. High-Level Architecture

---

### Backend Architecture

```text
API Layer → Application Layer → Domain Layer
        ↘ Infrastructure Layer (Database)
```

* API → handles HTTP requests
* Application → business logic (future)
* Domain → entities (Product, User)
* Infrastructure → database (EF Core + SQL Server)

---

### Frontend Architecture

```text
src/app
│
├── core
│   ├── services
│   ├── guards
│   └── interceptors
│
├── shared
│   ├── components
│   ├── models
│   └── utils
│
└── features
    ├── dashboard
    ├── products
    ├── auth
    └── reports
```

* core → global logic (API service, auth, interceptors)
* shared → reusable components and models
* features → business modules

---

## 4. Full Stack Request Flow

```text
User (Browser)
↓
Angular Component
↓
ApiService
↓
HttpClient
↓
ASP.NET Core API
↓
Middleware (Exception + Logging)
↓
Controller
↓
DbContext (EF Core)
↓
Database
↓
Response (JSON)
↓
Frontend UI Update
```

---

## 5. Frontend ↔ Backend Sequence Flow

```text
User opens Dashboard
↓
Angular Router loads Dashboard Component
↓
Component calls ApiService
↓
ApiService sends HTTP request (/api/health)
↓
Backend receives request
↓
Controller processes request
↓
Response returned (ApiResponse)
↓
Frontend receives response
↓
Signal updates
↓
UI updates automatically
```

---

## 6. Current Phase

The project has completed:

### Phase 1 — Backend Foundation 

* API setup
* middleware pipeline
* EF Core + database
* Dependency Injection
* logging and error handling

---

### Phase 2 — Frontend Foundation 

* Angular structure setup
* routing system
* feature-based architecture
* API service
* environment configuration
* frontend-backend integration
* strong typing with models
* product UI structure

---

## 7. Engineering Decisions

---

### Why ASP.NET Core + Angular

* strong backend ecosystem
* scalable frontend architecture
* widely used in industry

---

### Why Layered Architecture (Backend)

* separates responsibilities
* improves maintainability
* supports scalability

---

### Why Feature-Based Structure (Frontend)

* organizes code by business features
* easy to scale
* easy to understand

---

### Why ApiService

* centralizes API calls
* avoids repeated code
* easier maintenance

---

### Why Environment Configuration

* avoids hardcoded URLs
* supports development and production

---

### Why Strong Typing (Models)

* improves code safety
* reduces runtime errors
* provides better developer experience

---

### Why Middleware

* handles logging and errors centrally
* avoids repeated logic
* keeps controllers clean

---

## 8. Key Architectural Principles

* separation of concerns
* layered architecture
* dependency injection
* middleware-based request handling
* modular frontend structure
* strong typing
* reusable services

---

## 9. API and Response Design

All API responses follow a standard structure.

### Success Response

```json
{
  "success": true,
  "message": "Operation successful",
  "data": {},
  "errors": null
}
```

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
* predictable responses
* easier debugging
* cleaner API design

---

## 10. What I Learned

* how backend and frontend connect
* how Angular routing works
* how API communication works
* how to structure frontend architecture
* how middleware works in backend
* how to use Dependency Injection
* how to design scalable systems
* importance of strong typing
* importance of clean project structure

---

## 11. Why This Architecture Matters

This architecture ensures:

* clean and organized code
* easy debugging
* scalable design
* reusable structure
* production-ready foundation

---


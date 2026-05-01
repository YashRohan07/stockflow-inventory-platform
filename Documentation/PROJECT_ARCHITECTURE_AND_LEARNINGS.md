# PROJECT ARCHITECTURE AND LEARNINGS

---

## 1. Project Overview

### 1.1 What This Project Is

StockFlow is a full-stack Inventory & Product Management Platform built using ASP.NET Core and Angular.

It is designed as a learning-focused but industry-standard project that demonstrates backend architecture, frontend structure, authentication, reporting, and system design thinking.

---

### 1.2 Why This Project Was Built

* to learn full-stack development (backend + frontend)
* to understand real-world system architecture
* to practice clean code and SOLID principles
* to build a production-style full-stack application

---

### 1.3 Problem It Solves

* manage product records
* manage inventory and stock
* secure system using authentication
* control access using roles (Admin / Member)
* provide a structured and scalable system architecture
* support dynamic product exploration using search, filter, sort, and pagination
* generate reports and insights from inventory data

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
* Service → contains business logic (validation, rules, mapping, reporting)
* Repository → handles database operations
* Domain → core entities
* Infrastructure → database and external services

---

### 3.2 Frontend Architecture

```text
src/app

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
Service (Frontend)
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
Angular UI
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
Database
↓
Response
↓
Frontend UI Update
```

---

## 4.2 System Behavior and Data Integrity

The system ensures:

* SKU is unique across all products
* Quantity and price cannot be negative
* Invalid data is blocked at validation layer
* Database constraints provide an additional safety layer

### Failure Handling

* Invalid input → blocked by validation
* Unauthorized access → blocked by JWT + guards
* Server errors → handled by global exception middleware

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

## 6.1 Reporting Flow

```text
User selects report type
↓
Angular Reports Component
↓
ReportService (Frontend)
↓
HTTP Request
↓
ReportsController
↓
ReportService (Backend)
↓
ProductRepository
↓
Database
↓
Data Aggregation (Summary + Filtering)
↓
PDF Generator (optional)
↓
Response / File Download
↓
Frontend UI Update
```

This shows how reporting works from UI to backend and back.

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

* product CRUD APIs implemented
* DTO-based API design
* validation implemented
* SKU as unique identifier
* frontend product UI (list, create, edit, delete)
* Angular reactive forms
* full integration completed

---

### Phase 5 — Search, Filter, Sort, and Pagination

* dynamic listing using query parameters
* search (SKU, Name)
* date filtering
* sorting
* pagination
* paged API response
* frontend filter UI
* improved usability

---

### Phase 6 — Reporting and Advanced Features

* inventory reporting system implemented
* summary calculation (total products, quantity, average price, total value)
* low stock report with dynamic threshold
* PDF report generation from backend
* reports controller and service layer added
* DTO-based reporting response design
* frontend reports page implemented
* summary dashboard cards added
* low stock filtering UI
* PDF download integration (full report + low stock report)
* improved UX with loading states and messages

---

## 8. Key Engineering Decisions

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

* clean separation between internal data and API
* prevents direct exposure
* improves maintainability

---

### Use Validation Layer

* prevents invalid data
* improves UX
* ensures data integrity

---

### Use Angular Feature-Based Structure

* modular
* scalable

---

### Use Interceptors

* automatic token attachment
* cleaner API calls

---

### Use Guards

* route protection
* better UX

---

## 8.1 Design Trade-offs

### Not using full Clean Architecture

* simpler layered approach
* easier to understand
* avoids over-engineering

---

### No caching implemented

* simpler system
* acceptable for current scale

---

### No concurrency handling (yet)

* acceptable for low usage
* will improve later

---

## 9. Cross-Cutting Engineering Practices

* separation of concerns
* clean code principles
* validation strategy
* structured error handling
* logging
* security (JWT)
* consistent API design
* request tracing
* pagination for performance

---

## 10. Key Challenges and Solutions

### Challenge: Swagger token issue

**Solution:** fixed Bearer configuration

---

### Challenge: 401 vs 403 confusion

**Solution:** tested roles properly

---

### Challenge: frontend-backend integration

**Solution:** used services and models

---

### Challenge: route security

**Solution:** implemented guards

---

### Challenge: Angular UI update issues

**Solution:** used ChangeDetectorRef

---

### Challenge: Reporting summary mismatch

**Solution:** fixed dynamic threshold logic

---

## 11. What I Learned

### Backend

* JWT authentication
* middleware pipeline
* EF Core
* dependency injection
* reporting logic

---

### Frontend

* Angular architecture
* routing
* interceptors
* guards
* reactive forms
* report UI design

---

### Full Stack

* end-to-end system flow
* API communication
* DTO importance
* separation of layers

---

### System Design

* request flow
* stateless systems
* role-based access
* reporting systems
* data aggregation

---

## 12. Mistakes and Improvements

* initially no route protection
* login page accessible after login
* token expiry not handled
* reporting summary mismatch

**Improved by:**

* adding guards
* fixing login flow
* handling token expiry
* fixing reporting logic

---

## 13. Future Improvements

* advanced analytics (charts, trends)
* report caching (Redis)
* scheduled report generation
* export formats (CSV/Excel)
* background jobs (Hangfire)
* CI/CD pipeline
* cloud deployment (Azure)

---

## 14. System Readiness Level

Current system is:

* suitable for small-scale production
* supports authenticated users
* maintains data integrity
* includes basic reporting

Limitations:

* no advanced analytics
* no caching
* no background processing

---

## 15. Final Reflection

This project helped transition from coding to real engineering.

It improved:

* system thinking
* architecture understanding
* full-stack integration
* security implementation
* reporting and business logic understanding

# StockFlow - Inventory & Product Management Platform

StockFlow is a full-stack Inventory & Product Management Platform built using ASP.NET Core and Angular.

It is designed as a learning-first, industry-aligned project focused on backend architecture, frontend structure, system design, and scalable development practices.

---

## Project Overview

StockFlow provides a structured system to manage product and inventory data with role-based access and reporting capabilities.

The project is being developed phase-by-phase to ensure strong understanding of:

- clean architecture
- scalable system design
- real-world backend engineering
- frontend-backend integration

---

## Problem It Solves

- manage product records  
- manage inventory and stock data  
- provide role-based access 
- support reporting 
- ensure structured and scalable data handling  

---

## Technology Stack

### Backend

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- JWT Authentication 

---

### Frontend

- Angular (Standalone)  
- TypeScript  
- Angular Router  
- HttpClient  

---

## Architecture

### Backend (Layered Architecture)

```

API Layer → Application Layer → Domain Layer
↘ Infrastructure Layer (Database)

```

- API Layer → handles HTTP requests and responses  
- Application Layer → business logic 
- Domain Layer → core entities (Product, User)  
- Infrastructure Layer → database and external services  

---

### Frontend (Feature-Based Architecture)

```

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

- core → global logic (API service, auth, interceptors)  
- shared → reusable components and models  
- features → business modules  

---

## Full Stack Request Flow

```

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

## Development Roadmap

The project is being developed in structured phases:

* Phase 0 — Foundation, Planning, and Architecture
* Phase 1 — Backend Foundation
* Phase 2 — Frontend Foundation
* Phase 3 — Authentication and Authorization
* Phase 4 — Product and Inventory Management (CRUD)
* Phase 5 — Product Listing, Search, Filter, Sort, and Pagination
* Phase 6 — Inventory Reporting and Advanced Features
* Phase 7 — Error Handling, Logging, and Quality Improvements
* Phase 8 — Testing
* Phase 9 — Deployment and Production Readiness
* Phase 10 — Final Review and GitHub Polish

---

## Learning Focus

- backend architecture  
- frontend architecture  
- system design fundamentals  
- clean code structure  
- modular development  
- API design  
- full-stack integration  

---

## How to Run the Project

### Prerequisites

Make sure you have installed:

- .NET SDK  
- Node.js  
- Angular CLI  

---

### 1. Clone the repository

```

git clone [https://github.com/YashRohan07/stockflow-inventory-platform.git](https://github.com/YashRohan07/stockflow-inventory-platform.git)
cd stockflow-inventory-platform

```

---

### 🔹 2. Run Backend (Terminal 1)

```

dotnet run --project backend/StockFlow.API/StockFlow.API.csproj

```

You should see:

```

Now listening on: [http://localhost:5118](http://localhost:5118)

```

---

### 3. Run Frontend (Terminal 2)

```

cd frontend/stockflow-ui
ng serve

```

Open browser:

```

[http://localhost:4200](http://localhost:4200)

```

---

## Key Concepts Applied

* layered architecture
* separation of concerns
* dependency injection
* middleware pipeline
* feature-based frontend structure
* reusable API services
* strong typing (TypeScript models)

---

## Why This Project Matters

This project demonstrates:

* real-world backend architecture
* scalable frontend structure
* clean code practices
* full-stack integration
* system design thinking

---

## Author

**Yash Rohan**
Software Developer (ASP.NET Core, Angular, SQL)



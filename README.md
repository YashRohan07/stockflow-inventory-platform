# StockFlow - Inventory & Product Management Platform

![.NET](https://img.shields.io/badge/.NET-10-blue)
![Angular](https://img.shields.io/badge/Angular-17-red)
![License](https://img.shields.io/badge/license-MIT-green)

StockFlow is a **full-stack Inventory & Product Management Platform** built using **ASP.NET Core (Backend)** and **Angular (Frontend)**.

It is designed as a **production-style, scalable system** demonstrating **clean architecture, system design, performance optimization, and full-stack engineering practices**.

---

### Screenshots

#### Login
<img width="1892" height="901" alt="1" src="https://github.com/user-attachments/assets/ce2675a0-bc96-4ef1-89aa-2c9529293d1f" />


#### Product Inventory
<img width="1902" height="1042" alt="2" src="https://github.com/user-attachments/assets/3395e474-921f-47d1-b7e8-eb079a8477ba" />

<img width="1898" height="1046" alt="3" src="https://github.com/user-attachments/assets/64d30cde-0064-4d78-acec-73ac4a5a0eb7" />


#### Reports Dashboard

<img width="1899" height="1041" alt="4" src="https://github.com/user-attachments/assets/7493c880-ec18-4cde-9a4d-43b368b864c6" />


#### PDF Report
<img width="634" height="896" alt="5" src="https://github.com/user-attachments/assets/d91bd1e1-6ce7-4afa-bec3-de3a63030dd9" />
---
<img width="655" height="909" alt="7" src="https://github.com/user-attachments/assets/11f5eec3-7cbe-4e6a-ae61-665db32de093" />

---

## Why This Project Matters

Most inventory systems stop at CRUD.

StockFlow goes further by implementing:

- real-world backend layering (Controller → Service → Repository)
- performance-aware querying (filter, sort, pagination)
- reporting layer with aggregation logic
- PDF export using backend-generated documents

This demonstrates how production-grade backend systems are structured beyond basic CRUD applications.

---

## Project Highlights

✔ Layered backend architecture  
✔ JWT Authentication + Role-Based Access Control (Admin / Member)  
✔ Advanced product listing (search, filter, sort, pagination)  
✔ Inventory reporting system with PDF export  
✔ Global error handling & request logging (middleware)  
✔ Database optimization (indexing + stored procedure + raw SQL)  
✔ Angular feature-based architecture with interceptors & guards  

---

## Backend Architecture

StockFlow follows a layered architecture aligned with Clean Architecture principles:

API Layer → Application Layer → Domain Layer  
                ↓  
        Infrastructure Layer  

**Flow:**  
Controller → Service → Repository → Database

---

## Frontend Architecture

```

src/app
├── core (services, guards, interceptors)
├── shared (models, utilities)
└── features
├── auth
├── products
└── reports

```

---

## Full Stack Flow

```

User → Angular UI → API Service → ASP.NET API
→ Middleware → Controller → Service → Repository → Database
→ Response → UI Update

```

---

## Tech Stack

### Backend

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- JWT Authentication  
- Middleware (Exception + Logging)  
- Stored Procedures + Raw SQL  

### Frontend

- Angular (Standalone Architecture)  
- TypeScript  
- Angular Router  
- HttpClient  
- Reactive Forms  
- Interceptors (Auth + Error Handling)

## Tech Versions
- .NET: 10.0
- EF Core: 10.0.7
- Angular: 17
- Node.js: 18+ 
- NPM: 9+

---

## Core Features

### Authentication & Authorization

- JWT-based authentication  
- Role-based access (Admin / Member)  
- Route protection using Angular guards  

---

### Product & Inventory Management

- Create, update, delete products  
- SKU-based unique identification  
- Quantity and price tracking  

---

### Advanced Listing

- search (SKU, name)  
- filter (date range)  
- sort (price, quantity, date)  
- pagination  

---

### Reporting System

- full inventory report  
- low stock report (dynamic threshold)  
- summary analytics:
  - total products  
  - total quantity  
  - average price  
  - total inventory value  

---

### PDF Export

- downloadable reports  
- clean tabular format  
- backend-generated using QuestPDF  

---

### Error Handling & Logging

- global exception middleware  
- structured API responses  
- request logging with execution time  

---

### Database Optimization

- indexing (Name, Quantity, PurchaseDate)  
- stored procedure for low stock queries  
- raw SQL integration (FromSqlRaw)  
- database-level aggregation  

---

## System Design Considerations

- Strict separation of concerns across API, application, and data layers  
- Query optimization using indexing  
- Aggregation handled at database level  
- Stateless authentication using JWT  
- Scalable API structure with versioning readiness  

---

## Key Engineering Decisions

- Adopted layered architecture to ensure maintainability and scalability  
- Chose JWT for stateless authentication and better scalability  
- Implemented pagination to prevent large dataset performance issues  
- Used stored procedures for optimized reporting queries  

---

## API Endpoints

### Auth
```

POST /api/auth/login

```

### Products
```

GET    /api/products
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}

```

### Reports
```

GET /api/reports/summary
GET /api/reports/inventory
GET /api/reports/low-stock
GET /api/reports/inventory/pdf
GET /api/reports/low-stock/pdf

```

---

## Project Structure

```

/backend
├── API
├── Application
├── Domain
└── Infrastructure

/frontend
└── Angular UI

/Documentation
└── Phase-based docs & screenshots

````

---

## How to Run Locally

### Prerequisites

- .NET SDK  
- Node.js  
- Angular CLI  

---

### 1. Clone Repository

```bash
git clone https://github.com/YashRohan07/stockflow-inventory-platform.git
cd stockflow-inventory-platform
````

---

### 2. Run Backend

```bash
dotnet run --project backend/StockFlow.API/StockFlow.API.csproj
```

Backend runs on:

```
http://localhost:5118
```

---

### 3. Run Frontend

```bash
cd frontend/stockflow-ui
ng serve
```

Open:

```
http://localhost:4200
```

---

## System Readiness

✔ Production-style architecture
✔ Performance-aware design
✔ Structured error handling
✔ Reporting + analytics foundation

---

## Future Improvements

* Dockerize backend and frontend for environment consistency
* Deploy to Azure (App Service + SQL Database)
* Introduce background jobs (Hangfire) for async report generation
* Add Redis caching for frequently accessed report data
* Implement unit and integration testing (xUnit + Angular testing)
* Build real-time dashboard analytics (SignalR)

---

## Author

**Yash Rohan**
Software Developer | ASP.NET Core | Angular | SQL Server


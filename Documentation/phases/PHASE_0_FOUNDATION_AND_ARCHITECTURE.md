# Phase 0 — Foundation, Planning, and Architecture

## 1. Phase Objective

The goal of this phase was to properly define the engineering foundation of the StockFlow project so that future development can be clean, scalable, and maintainable.

This phase focused on:

* project structure setup
* defining a clean layered backend architecture
* frontend base setup
* identifying core entities
* planning role-based system
* setting up documentation

---

## 2. What Was Built

No actual features were implemented in this phase.
Instead, the foundation of the project was established.

### Backend

* ASP.NET Core solution structure created
* API, Application, Domain, Infrastructure projects created
* project references configured
* internal folder structure created

### Frontend

* Angular application created
* base frontend structure initialized

### Documentation

* Documentation folder created
* phase-based documentation structure created
* architecture and learning document initialized

### Domain Design

* Product entity designed
* User entity designed
* UserRole enum defined

---

## 3. Backend Work Done

* `StockFlow.slnx` created
* `StockFlow.API`, `Application`, `Domain`, `Infrastructure` projects created
* projects added to solution
* references configured:

  * API → Application
  * Application → Domain
  * Infrastructure → Application
  * Infrastructure → Domain
* core entities created in Domain layer
* internal folders prepared for controllers, services, repositories, etc.

---

## 4. Frontend Work Done

* Angular app `stockflow-ui` created
* routing enabled
* SCSS selected
* project prepared for future feature-based structure

---

## 5. What I Learned

* importance of defining architecture before coding
* how solution structure affects maintainability
* practical use of layered architecture
* benefits of separating frontend and backend
* importance of early domain modeling
* value of maintaining documentation from the beginning

---

## 6. System Design Concepts Covered

* system design basics
* high-level architecture planning
* layered architecture
* separation of concerns
* modular boundaries
* domain vs infrastructure
* request flow basics
* role-based system thinking

---

## 7. SOLID / Design Principles Applied

### Single Responsibility Principle (SRP)

Each layer has a clear responsibility:

* API → request handling
* Application → business logic
* Domain → entities
* Infrastructure → external concerns

### Separation of Concerns

Backend and frontend are separated, and backend is divided into logical layers.

---

## 8. Key Engineering Decisions

### Use layered architecture instead of single project

**Why:**

* better maintainability
* clean structure
* easier scalability
* more professional design

---

### Use ASP.NET Core + Angular

**Why:**

* strong backend support
* structured frontend
* suitable for dashboard-style applications

---

### Design entities early

**Why:**

* helps API design
* reduces confusion in later phases

---

## 9. Alternatives Considered

### Single project backend

* simple but not scalable

### Full clean architecture (CQRS)

* powerful but too complex for early phase

**Final choice:** balanced layered architecture

---

## 10. Why This Phase Matters

Without proper structure, projects become messy quickly.
This phase ensures:

* clean feature development
* better debugging
* scalability
* professional project structure

---

## 11. Challenges Faced

* understanding layer separation
* deciding responsibilities
* fixing NuGet issues
* avoiding over-engineering

---

## 12. How They Were Solved

* followed layered structure
* fixed NuGet source
* defined entities early
* kept architecture simple

---

## 13. Industry Practices Followed

* clean project structure
* separation of concerns
* documentation-first approach
* domain-driven thinking

---

## 14. Topics Covered

* architecture fundamentals
* solution structuring
* frontend setup
* domain modeling
* documentation

---

## 15. Next Phase Preparation

This phase prepares for:

* backend API development
* database integration
* middleware setup
* authentication
* CRUD implementation

---


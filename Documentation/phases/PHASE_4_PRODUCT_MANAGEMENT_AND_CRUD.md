# Phase 4 — Product and Inventory Management (CRUD)

---

## 1. Phase Objective

The goal of this phase was to build the **core business module** of StockFlow:

Product and Inventory Management

This phase focuses on:

* creating full CRUD (Create, Read, Update, Delete) functionality
* designing proper API contracts using DTOs
* implementing clean service and repository layers
* adding validation for safe data handling
* connecting frontend with backend APIs
* building real UI for product management

This is the phase where the system becomes **actually usable**.

---

## 2. What Was Built

### Backend

* Product entity fully defined
* Database configuration for Product
* EF Core migration and database update
* DTOs for Create, Update, and Response
* Validation using validators
* ProductRepository implemented
* ProductService implemented
* Full CRUD APIs:

  * GET all products
  * GET product by id
  * POST create product
  * PUT update product
  * DELETE product

---

### Frontend

* Product List page
* Product Form (Create + Edit)
* Delete functionality
* API integration using ProductService
* Reactive Forms used
* Routing for create/edit pages
* Loading, error, and success handling

---

## 3. Backend Work Done

### 🔹 Product Entity Design

* Defined fields:

  * SKU
  * Name
  * Size
  * Color
  * Quantity
  * PurchasePrice
  * PurchaseDate

* Added BaseEntity for Id

Now Product represents a real inventory item.

---

### 🔹 ProductConfiguration (Database Rules)

* SKU required + unique
* Name required
* Quantity ≥ 0
* PurchasePrice ≥ 0
* Decimal precision set
* PurchaseDate required

This ensures **data integrity at database level**

---

### 🔹 EF Core Migration

Commands used:

```bash
dotnet ef migrations add AddProductCrudFields
dotnet ef database update
```

👉 Result:

* Products table created/updated
* Constraints applied

---

### 🔹 DTOs 

Created:

* CreateProductDto
* UpdateProductDto
* ProductResponseDto

Why:

* prevent exposing entity directly
* define clean API contract
* separate input vs output

---

### 🔹 Validation Layer

* CreateProductValidator
* UpdateProductValidator

Rules:

* SKU required (only create)
* Name required
* Quantity cannot be negative
* Price cannot be negative

Validation happens **before database**

---

### 🔹 Repository Layer

Created:

* IProductRepository
* ProductRepository

Methods:

* GetAllAsync()
* GetByIdAsync()
* GetBySkuAsync()
* SkuExistsAsync()
* AddAsync()
* UpdateAsync()
* DeleteAsync()

Repository handles **only database operations**

---

### 🔹 Service Layer (System Brain)

Created:

* IProductService
* ProductService

Responsibilities:

* business logic
* SKU uniqueness check
* validation integration
* mapping DTO ↔ Entity

Flow:

```
Controller → Service → Repository → DB
```

---

### 🔹 Controller (API Layer)

Endpoints:

```
GET    /api/products
GET    /api/products/{id}
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}
```

Uses ApiResponse<T> for consistent output

---

## 4. Frontend Work Done

### 🔹 Product List Page

* Displays products in table

* Shows:

  * SKU
  * Name
  * Size
  * Color
  * Quantity
  * Price
  * Date

* Handles:

  * loading state
  * empty state

---

### 🔹 Product Form (Create + Edit)

* Reactive Form used

* Fields:

  * SKU
  * Name
  * Size
  * Color
  * Quantity
  * Price
  * Date

* Edit mode:

  * loads existing product
  * disables SKU

---

### 🔹 API Integration

* ProductService created in Angular
* Uses HttpClient

Methods:

* getAllProducts()
* getProductById()
* createProduct()
* updateProduct()
* deleteProduct()

---

### 🔹 Routing

Routes added:

```
/products
/products/create
/products/edit/:id
```

---

### 🔹 User Actions

* Add product
* Edit product
* Delete product
* View product list

---

## 5. What I Learned

* how real CRUD systems work
* importance of DTO separation
* difference between service and repository
* how validation improves data safety
* how frontend interacts with backend APIs
* how to design API contracts
* how to manage form state in Angular
* how to structure full-stack feature

---

## 6. System Design and Architecture Concepts Covered

---

### 🔹 6.1 CRUD Flow

```
User → UI → API → Controller → Service → Repository → DB
```

---

### 🔹 6.2 Layer Responsibility

* Controller → handles request
* Service → business logic
* Repository → database
* DTO → data contract

---

### 🔹 6.3 DTO-Based API Design

```
Frontend → CreateProductDto → Backend
Backend → ProductResponseDto → Frontend
```

prevents data leakage

---

### 🔹 6.4 Validation Flow

```
Request → Validator → Service → Repository → DB
```

bad data blocked early

---

### 🔹 6.5 Business Identity vs Database Identity

* Id → database identity
* SKU → business identity

SKU must be unique

---

### 🔹 6.6 Full Stack Flow

```
Angular UI
↓
ProductService
↓
HTTP Request
↓
ASP.NET Controller
↓
Service Layer
↓
Repository
↓
SQL Server
↓
Response
↓
UI Update
```

---

## 7. SOLID / Design Principles Applied

---

### 🔹 SRP (Single Responsibility)

* Controller → request handling
* Service → logic
* Repository → DB

---

### 🔹 DIP (Dependency Inversion)

* Service depends on interface (IProductRepository)
* not concrete class

---

### 🔹 DRY

* validation centralized
* repository reusable

---

### 🔹 Separation of Concerns

* UI, API, DB separated clearly

---

## 8. Key Engineering Decisions

---

### Use DTO instead of Entity

**Why:**

* prevents exposing internal structure
* safer API
* better flexibility

---

### Use Service Layer

**Why:**

* keeps business logic separate
* reusable logic
* cleaner controller

---

### Use Repository Pattern

**Why:**

* separates DB logic
* easier testing
* maintainable

---

### Use Validation Layer

**Why:**

* prevents bad data
* better user error messages

---

### Use SKU as Business Identity

**Why:**

* human-readable
* real-world product reference

---

## 9. Alternatives Considered

---

### Direct DbContext in Controller ❌

* messy
* no separation
* not scalable

---

### No DTO ❌

* unsafe
* exposes internal data

---

### No Validation ❌

* bad data in DB
* poor UX

---

## 10. Why This Phase Matters

This is the **most important phase so far**.

Before this:

* system was just structure

After this:

* system is functional 

Now you can:

* manage products
* track inventory
* perform real operations

This is where your project becomes **real software**

---

## 11. Challenges Faced

* understanding service vs repository
* designing DTOs correctly
* validation rules
* frontend-backend integration
* routing issues (double click bug)
* async data loading issues

---

## 12. How I Solved Them

* followed layered architecture strictly
* separated DTO clearly
* added validators
* fixed routing/navigation flow
* handled loading state properly
* tested APIs using Swagger

---

## 13. Industry-Standard Practices Followed

* layered architecture
* repository pattern
* service layer pattern
* DTO-based API design
* validation before DB
* clean API response structure
* reactive forms in frontend
* full-stack integration

---

## 14. Topics Covered After Completing This Phase

After this phase, I covered:

* CRUD operations
* DTO design
* service layer architecture
* repository pattern
* validation systems
* API design
* frontend-backend integration
* Angular reactive forms
* full-stack development


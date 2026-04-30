# Phase 5 — Search, Filter, Sort, and Pagination

---

## 1. Phase Objective

The goal of this phase was to make the product listing **usable, flexible, and production-ready**.

Before this phase:

* Products could only be viewed as a simple list

After this phase:

* Users can **search, filter, sort, and paginate** products efficiently

This phase solves a real-world problem:

When data grows, users must be able to quickly **find and organize information**

---

## 2. What Was Built

### Backend

* Query-based API for product listing
* Global search (SKU, Name)
* Date range filtering (Purchase Date)
* Sorting (Date, Price, Quantity)
* Pagination support
* Paged API response

---

### Frontend

* Search input (SKU / Name)
* Date filters (From / To)
* Sort dropdowns
* Pagination UI
* API integration using query parameters

---

## 3. Backend Work Done

### 🔹 ProductQueryParametersDto

Created a DTO to handle all query parameters:

* Search
* PurchaseDateFrom
* PurchaseDateTo
* SortBy
* SortOrder
* Page
* PageSize

This makes API flexible and clean

---

### 🔹 Search Implementation

Search works on:

* SKU
* Name

Example:

```csharp
query = query.Where(p =>
    p.SKU.Contains(search) ||
    p.Name.Contains(search));
```

Allows users to quickly find products

---

### 🔹 Filtering Implementation

Filtering by purchase date:

* From date
* To date

Example:

```csharp
if (from != null)
    query = query.Where(p => p.PurchaseDate >= from);

if (to != null)
    query = query.Where(p => p.PurchaseDate <= to);
```

Useful for inventory tracking

---

### 🔹 Sorting Implementation

Sorting options:

* Newest first (PurchaseDate DESC)
* Oldest first (ASC)
* Price low → high
* Quantity high → low

Helps users analyze data easily

---

### 🔹 Pagination Implementation

Pagination logic:

```csharp
var total = await query.CountAsync();

var items = await query
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

Only required data is fetched → improves performance

---

### 🔹 PagedResponse<T>

Response structure:

```json
{
  "items": [...],
  "totalCount": 50,
  "page": 1,
  "pageSize": 10
}
```

Frontend knows:

* how many items exist
* how many pages to show

---

## 4. Frontend Work Done

### 🔹 Search UI

* Input field:

  * "Search by SKU or product name"

Sends query param to backend

---

### 🔹 Date Filters

* Purchase Date From
* Purchase Date To

Helps filter inventory by time

---

### 🔹 Sorting Controls

Dropdowns:

* Sort By (Date, Price, Quantity)
* Sort Order (Ascending / Descending)

---

### 🔹 Pagination UI

* Page number display
* Page size selector
* Next / Previous buttons

---

### 🔹 API Integration

Frontend sends query like:

```
/api/products?search=shirt&page=1&pageSize=10&sortBy=price&sortOrder=asc
```

Backend handles everything dynamically

---

## 5. What I Learned

* difference between search, filter, and sort
* how to design flexible APIs using query parameters
* how pagination improves performance
* how frontend and backend work together for dynamic data
* how to handle user-driven data queries
* importance of efficient data retrieval

---

## 6. System Design and Architecture Concepts Covered

---

### 🔹 6.1 Query-Based API Design

Instead of fixed endpoints:

```
/api/products
```

We use:

```
/api/products?search=shirt&page=1&pageSize=10
```

More flexible and scalable

---

### 🔹 6.2 Separation of Concerns

* Search → finding data
* Filter → narrowing data
* Sort → ordering data

Each has a separate responsibility

---

### 🔹 6.3 Data Flow

```
Frontend UI
↓
Query Params
↓
Controller
↓
Service
↓
Repository
↓
Database
↓
Paged Response
↓
UI Update
```

---

### 🔹 6.4 Efficient Data Retrieval

Instead of loading all data:

Load only needed data using pagination

---

### 🔹 6.5 Scalability Thinking

If products = 10,000+

Without pagination ❌ slow
With pagination ✅ fast

---

## 7. SOLID / Design Principles Applied

---

### 🔹 SRP (Single Responsibility)

* Controller → request handling
* Service → business logic
* Repository → DB queries

---

### 🔹 DRY

* Query logic reused
* No duplicate filtering logic

---

### 🔹 Separation of Concerns

* UI ≠ API ≠ DB logic

---

### 🔹 KISS

* simple filtering logic
* no over-complex query system

---

## 8. Key Engineering Decisions

---

### Use Query Parameters

**Why:**

* flexible API
* easy to extend
* industry standard

---

### Limit Search to SKU + Name

**Why:**

* fast queries
* avoids complexity
* most useful fields

---

### Use Date Filtering

**Why:**

* inventory is time-based
* helps tracking purchases

---

### Use Pagination

**Why:**

* performance improvement
* reduces DB load
* better UX

---

## 9. Alternatives Considered

---

### Load all products ❌

* slow
* bad performance

---

### Multiple APIs for each filter ❌

* messy
* not scalable

---

### Complex search engine ❌

* over-engineering
* unnecessary for this system

---

Final choice: **simple + flexible query system**

---

## 10. Why This Phase Matters

Before this phase:

* System was functional

After this phase:

* System is **usable in real-world**

Now users can:

* find products quickly
* analyze inventory
* navigate large data

This is a **production-level feature**

---

## 11. Challenges Faced

* handling multiple query parameters
* combining search + filter + sort
* pagination logic
* frontend state management
* keeping UI clean

---

## 12. How I Solved Them

* created Query DTO
* handled logic step-by-step
* tested with different scenarios
* simplified UI controls
* verified API responses

---

## 13. Industry-Standard Practices Followed

* query-based APIs
* pagination
* clean separation of logic
* DTO usage
* efficient database querying
* scalable API design

---

## 14. Topics Covered After Completing This Phase

After this phase, I covered:

* search implementation
* filtering logic
* sorting strategies
* pagination
* query-based API design
* scalable listing systems

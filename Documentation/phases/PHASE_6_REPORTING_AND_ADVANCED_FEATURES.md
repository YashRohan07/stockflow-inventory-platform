# PHASE 6 — Reporting and Advanced Features

---

## 1. Phase Objective

The goal of this phase was to add **reporting capability** to the system.

In previous phases:

* We could **store and manage products**
* We could **search, filter, and view data**

But there was a missing part:
No way to **analyze inventory data in a summarized way**

So in this phase, we solved:

* How to **generate inventory reports**
* How to **summarize data (total, average, value)**
* How to **export reports as PDF**

---

## 2. What Was Built

### Backend

* Inventory report API
* Low stock report API
* Report summary calculation
* PDF report generation
* Report service layer

### Frontend

* Reports page UI
* Summary dashboard cards
* Low stock filter
* PDF download buttons
* Dynamic report loading

---

## 3. Backend Work Done

### 3.1 Report DTOs Created

We created:

* `InventoryReportItemDto`
* `InventorySummaryDto`

**Why:**

* We do not return raw entity data
* We return clean and structured report data

---

### 3.2 Report Service Implemented

File:

```
StockFlow.Application/Services/ReportService.cs
```

This service:

* Fetches products
* Calculates summary data
* Applies low stock filtering
* Prepares report output

---

### 3.3 Summary Logic Added

We calculate:

* Total Products
* Total Quantity
* Average Price
* Total Inventory Value
* Low Stock Products

Example:

```
TotalInventoryValue = Quantity × Price
```

---

### 3.4 Dynamic Low Stock Logic

Low stock is not hardcoded anymore.

Now it uses user input:

```
LowStockProducts = products.Count(p => p.Quantity < threshold)
```

**Why this matters:**

* More flexible
* Matches real-world usage

---

### 3.5 Reports Controller Created

File:

```
StockFlow.API/Controllers/ReportsController.cs
```

Endpoints:

* `/api/reports/summary`
* `/api/reports/inventory`
* `/api/reports/low-stock`
* `/api/reports/inventory/pdf`
* `/api/reports/low-stock/pdf`

This exposes reporting features to frontend.

---

### 3.6 PDF Generator Implemented

Files:

* `IPdfReportGenerator.cs`
* `PdfReportGenerator.cs`

Responsibilities:

* Format report data
* Generate PDF
* Return downloadable file

---

### 3.7 Dependency Injection Setup

Registered:

* `IReportService`
* `IPdfReportGenerator`

**Why:**

* Loose coupling
* Clean architecture
* Easy testing

---

## 4. Frontend Work Done

### 4.1 Reports Page Created

Path:

```
features/reports
```

Used for:

* Viewing reports
* Downloading PDFs

---

### 4.2 Summary Cards UI

Displayed:

* Total Products
* Total Quantity
* Average Price
* Total Inventory Value
* Low Stock Products

Gives quick business insights

---

### 4.3 Low Stock Filter UI

User can:

* Input threshold
* View filtered results

Fully dynamic behavior

---

### 4.4 Report Table UI

Displays:

* SKU
* Name
* Quantity
* Price
* Total Value
* Purchase Date

Clean and readable structure

---

### 4.5 PDF Download Integration

Buttons:

* Full Report PDF
* Low Stock PDF

Calls backend and downloads file

---

## 5. What I Learned

From this phase, I learned:

* How to build a **reporting system**
* How to calculate **aggregated data**
* How to generate **PDF from backend**
* How frontend handles **file download**
* How to implement **dynamic filtering**

---

## 6. System Design and Architecture Concepts Covered

### 6.1 Data Aggregation

* Summarizing raw data into insights

---

### 6.2 Layer Responsibility

Flow:

```
Controller → Service → Repository → Database
```

* Controller → handles request
* Service → business logic
* Repository → data access

---

### 6.3 DTO Design

* Separate entity from response
* Prevents unnecessary data exposure

---

### 6.4 Client-Server Flow

```
Frontend → API → Service → Data → Response → UI
```

---

### 6.5 Export System Design

* Backend generates file
* Frontend downloads it

Real-world enterprise pattern

---

## 7. SOLID / Design Principles Applied

### SRP (Single Responsibility)

* ReportService → only reporting logic
* PdfGenerator → only PDF generation

---

### DIP (Dependency Inversion)

* Services depend on interfaces, not implementations

---

### Separation of Concerns

* UI ≠ Business Logic ≠ Database

---

### Layered Architecture

* Clean separation between layers

---

## 8. Key Engineering Decisions

### Decision: Use Service Layer for Reporting

**Why:**

* Keeps controller clean
* Centralizes logic
* Easy to maintain

---

### Decision: Generate PDF in Backend

**Why:**

* More secure
* Full control over format
* Less frontend complexity

---

### Decision: Use Dynamic Threshold

**Why:**

* Flexible
* User-driven
* Real-world behavior

---

## 9. Alternatives Considered

### Option 1: Generate PDF in Frontend

* ❌ Hard to maintain
* ❌ Limited control

---

### Option 2: Static Threshold

* ❌ Not flexible

---

### Final Choice:

✔ Backend PDF + Dynamic Threshold

---

## 10. Why This Matters in Real Systems

Reporting is **very important** in real applications.

Used for:

* Business decisions
* Inventory monitoring
* Financial insights

Without reporting, system is incomplete

---

## 11. Challenges Faced

* Summary mismatch issue
* Threshold logic bug
* PDF formatting issues
* UI consistency

---

## 12. How I Solved Them

* Fixed threshold logic
* Improved PDF structure
* Added dynamic UI updates
* Cleaned UI formatting

---

## 13. Industry-Standard Practices Followed

* Clean architecture
* DTO-based API design
* Service layer pattern
* Backend file generation
* Separation of concerns

---

## 14. Topics Covered After Completing This Phase

After this phase, I covered:

* Reporting system design
* Data aggregation
* PDF generation
* Backend file handling
* Advanced frontend-backend integration


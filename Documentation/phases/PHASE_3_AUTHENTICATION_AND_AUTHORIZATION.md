# Phase 3 — Authentication and Authorization

---

## 1. Phase Objective

The goal of this phase was to secure the StockFlow application using proper authentication and authorization.

This phase focuses on:

* implementing user login system
* securing APIs using JWT (JSON Web Token)
* handling password hashing
* implementing role-based access control (Admin / Member)
* protecting backend APIs
* integrating authentication in frontend (Angular)
* managing user session and token lifecycle

---

## 2. What Was Built

### Backend

* Login API (`POST /api/Auth/login`)
* Password hashing and verification
* JWT token generation
* Claims-based authentication
* Role-based authorization (Admin / Member)
* Authorization policies
* Protected test endpoints:

  * `/protected`
  * `/admin-only`
  * `/admin-or-member`
* Swagger Bearer authentication support
* Custom UnauthorizedException
* Improved global exception handling

---

### Frontend

* Login page UI
* AuthService for authentication
* Token storage in localStorage
* Auto redirect after login
* Route protection using Auth Guard
* Login page blocking using Login Guard
* HTTP Interceptor for attaching JWT token
* Logout functionality
* Basic token expiry handling

---

## 3. Backend Work Done

* Created `AuthController` with login endpoint
* Implemented `AuthService`
* Added password hashing using `IPasswordHasher`
* Implemented JWT token generation using `JwtTokenGenerator`
* Configured JWT settings in `appsettings.json`
* Bound JWT settings using `JwtOptions`
* Configured authentication middleware in `Program.cs`
* Added authorization policies:

  * AdminOnly
  * AdminOrMember
* Created test endpoints for role verification
* Fixed Swagger Bearer token issue
* Enabled Swagger Authorization support
* Created `UnauthorizedException`
* Updated `ExceptionMiddleware` for structured error response

---

## 4. Frontend Work Done

* Created Login component
* Built login form (email, password)
* Connected login UI with backend API
* Created AuthService:

  * login()
  * logout()
  * getToken()
  * getUser()
  * isLoggedIn()
* Stored token, user, expiresAt in localStorage
* Implemented redirect after login → `/dashboard`
* Created Auth Guard:

  * protects dashboard, products, reports
* Created Login Guard:

  * blocks `/login` if already logged in
* Created Auth Interceptor:

  * attaches JWT token to every request
* Implemented logout button
* Added basic token expiry check

---

## 5. What I Learned

* how authentication works in real applications
* difference between authentication and authorization
* how JWT works internally
* how claims-based identity works
* how role-based access control is implemented
* how frontend and backend work together in auth flow
* importance of securing APIs
* how to manage user sessions in frontend
* how interceptors simplify API security
* how guards protect frontend routes

---

## 6. System Design and Architecture Concepts Covered

---

### 6.1 Authentication vs Authorization

* Authentication = who the user is (login)
* Authorization = what the user can do (permissions)

---

### 6.2 JWT Authentication Flow

```
User enters email + password
↓
Frontend sends request to backend
↓
Backend verifies credentials
↓
JWT token generated
↓
Token returned to frontend
↓
Frontend stores token
↓
Token sent with every request
↓
Backend validates token
↓
Access granted or denied
```

---

### 6.3 Stateless System

* Backend does NOT store session
* Every request contains token
* Server validates token each time

---

### 6.4 Claims-Based Identity

JWT token contains:

* user id
* name
* email
* role

Example:

```
Role = Admin / Member
```

Backend reads claims to decide access.

---

### 6.5 Role-Based Access Control

```
Admin → full access
Member → limited access
```

Example:

* `/admin-only` → Admin only
* `/admin-or-member` → both allowed

---

### 6.6 Frontend Authentication Flow

```
Login form
↓
AuthService
↓
API call
↓
Token received
↓
Stored in localStorage
↓
Interceptor attaches token
↓
Guard protects routes
↓
User navigates app
```

---

### 6.7 Token Lifecycle

```
Login → Token created
↓
Stored in browser
↓
Used in API requests
↓
Expires after time
↓
User logged out
```

---

## 7. SOLID / Design Principles Applied

---

### Single Responsibility Principle (SRP)

* AuthService → handles auth logic
* JwtTokenGenerator → handles token creation
* Middleware → handles errors

---

### Separation of Concerns

* Backend handles security
* Frontend handles UI + session

---

### DRY (Don’t Repeat Yourself)

* Interceptor avoids repeating token logic
* Middleware avoids repeating error handling

---

### KISS (Keep It Simple)

* simple JWT implementation
* basic role system

---

## 8. Key Engineering Decisions

---

### Use JWT Authentication

**Why:**

* stateless
* scalable
* works well with Angular SPA

---

### Use Role-Based Authorization

**Why:**

* simple and effective
* easy to extend
* real-world standard

---

### Use Interceptor

**Why:**

* automatic token attachment
* cleaner code

---

### Use Guards

**Why:**

* protect frontend routes
* improve user experience

---

## 9. Alternatives Considered

---

### Session-Based Authentication

❌ Not scalable
❌ server memory dependent

---

### Cookie-Based Authentication

❌ complex with SPA
❌ CSRF issues

---

### No Authorization

❌ insecure
❌ not usable in real systems

---

## 10. Why This Phase Matters

Authentication is one of the most critical parts of any system.

This phase ensures:

* secure API access
* proper user control
* real-world system behavior
* foundation for all future features

---

## 11. Challenges Faced

* understanding JWT flow
* Swagger token not sending issue
* handling 401 vs 403
* frontend-backend integration
* token storage management
* route protection

---

## 12. How I Solved Them

* fixed Swagger Bearer configuration
* tested with Admin and Member roles
* implemented custom exception handling
* used interceptor for token
* added guards for route protection
* verified using frontend + backend testing

---

## 13. Industry-Standard Practices Followed

* JWT authentication
* role-based authorization
* stateless API design
* global exception handling
* structured error response
* frontend route protection
* secure API communication

---

## 14. Topics Covered After Completing This Phase

After this phase, I covered:

* JWT authentication
* authorization policies
* claims-based identity
* role-based access control
* frontend authentication flow
* route guards
* HTTP interceptors
* token lifecycle
* secure API design


namespace StockFlow.Domain.Enums;

// Defines roles used for authorization across the system.
// These values are used in JWT claims and policy-based access control.
public enum UserRole
{
    // Full access to system features (CRUD, reports, PDF export, etc.)
    Admin,

    // Limited access (read-only operations such as viewing products and reports)
    Member
}
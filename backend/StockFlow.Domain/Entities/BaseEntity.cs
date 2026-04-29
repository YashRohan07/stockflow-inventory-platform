namespace StockFlow.Domain.Entities;

// BaseEntity contains common fields for all entities.
// Product and User can inherit from this class.
public abstract class BaseEntity
{
    // Primary key.
    // The database will auto-generate this value.
    public int Id { get; set; }
}
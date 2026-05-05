using StockFlow.Application.DTOs.Products;

namespace StockFlow.Application.Validators;

// Performs manual validation for product creation input.
// Returns a list of validation error messages instead of throwing exceptions.
// This approach keeps validation simple and explicit without introducing external libraries.
public static class CreateProductValidator
{
    public static List<string> Validate(CreateProductDto dto)
    {
        var errors = new List<string>();

        // SKU: required and length constraint
        if (string.IsNullOrWhiteSpace(dto.SKU))
        {
            errors.Add("SKU is required.");
        }
        else if (dto.SKU.Length > 50)
        {
            errors.Add("SKU cannot be more than 50 characters.");
        }

        // Name: required and length constraint
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            errors.Add("Name is required.");
        }
        else if (dto.Name.Length > 150)
        {
            errors.Add("Name cannot be more than 150 characters.");
        }

        // Optional fields: validate only if provided
        if (!string.IsNullOrWhiteSpace(dto.Size) && dto.Size.Length > 50)
        {
            errors.Add("Size cannot be more than 50 characters.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Color) && dto.Color.Length > 50)
        {
            errors.Add("Color cannot be more than 50 characters.");
        }

        // Numeric validations (basic domain constraints)
        if (dto.Quantity < 0)
        {
            errors.Add("Quantity cannot be negative.");
        }

        if (dto.PurchasePrice < 0)
        {
            errors.Add("Purchase price cannot be negative.");
        }

        // Required date validation
        if (dto.PurchaseDate == default)
        {
            errors.Add("Purchase date is required.");
        }

        return errors;
    }
}
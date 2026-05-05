using StockFlow.Application.DTOs.Products;

namespace StockFlow.Application.Validators;

// Performs manual validation for product update input.
// SKU is intentionally excluded from update validation because it is not editable after creation.
public static class UpdateProductValidator
{
    public static List<string> Validate(UpdateProductDto dto)
    {
        var errors = new List<string>();

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

        // Numeric validations protect inventory state from invalid values
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
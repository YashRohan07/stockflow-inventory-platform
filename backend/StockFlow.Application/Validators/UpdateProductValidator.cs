using StockFlow.Application.DTOs.Products;

namespace StockFlow.Application.Validators;

// This validator checks product update input.
// It returns a list of validation error messages.
public static class UpdateProductValidator
{
    public static List<string> Validate(UpdateProductDto dto)
    {
        var errors = new List<string>();

        // Name validation
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            errors.Add("Name is required.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name.Length > 150)
        {
            errors.Add("Name cannot be more than 150 characters.");
        }

        // Size validation
        if (!string.IsNullOrWhiteSpace(dto.Size) && dto.Size.Length > 50)
        {
            errors.Add("Size cannot be more than 50 characters.");
        }

        // Color validation
        if (!string.IsNullOrWhiteSpace(dto.Color) && dto.Color.Length > 50)
        {
            errors.Add("Color cannot be more than 50 characters.");
        }

        // Quantity validation
        if (dto.Quantity < 0)
        {
            errors.Add("Quantity cannot be negative.");
        }

        // Purchase price validation
        if (dto.PurchasePrice < 0)
        {
            errors.Add("Purchase price cannot be negative.");
        }

        // Purchase date validation
        if (dto.PurchaseDate == default)
        {
            errors.Add("Purchase date is required.");
        }

        return errors;
    }
}
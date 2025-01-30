using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Validator for <see cref="CreateSaleItemRequest"/> ensuring valid input for creating a sale item.
    /// </summary>
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleItemRequestValidator"/> with validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - ProductName: Required, maximum length of 100 characters.
        /// - Quantity: Must be between 1 and 20.
        /// - UnitPrice: Must be greater than zero.
        /// </remarks>
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}

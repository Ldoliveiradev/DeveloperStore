using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Validator for <see cref="UpdateSaleItemRequest"/> that defines validation rules for updating a sale item.
    /// </summary>
    public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleItemRequestValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Required and must be a valid GUID
        /// - Product Name: Required, length between 1 and 100 characters
        /// - Quantity: Must be between 1 and 20
        /// - Unit Price: Must be greater than or equal to zero
        /// - IsCancelled: Required (bool)
        /// </remarks>
        public UpdateSaleItemRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale item ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid Sale item ID.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero.");

            RuleFor(x => x.IsCancelled)
                .NotNull().WithMessage("IsCancelled field is required.");
        }
    }
}

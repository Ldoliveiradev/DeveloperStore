using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Validator for <see cref="CreateSaleItemCommand"/> that defines validation rules for creating a sale item.
    /// </summary>
    /// <remarks>
    /// Validation rules ensure that:
    /// <list type="bullet">
    /// <item><description>Product name must be provided and cannot exceed 100 characters.</description></item>
    /// <item><description>Quantity must be between 1 and 20.</description></item>
    /// <item><description>Unit price must be greater than zero.</description></item>
    /// </list>
    /// </remarks>
    public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleItemCommandValidator"/> class with validation rules.
        /// </summary>
        public CreateSaleItemCommandValidator()
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

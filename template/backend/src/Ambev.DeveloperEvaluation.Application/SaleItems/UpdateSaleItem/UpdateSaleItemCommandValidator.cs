using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Validator for <see cref="UpdateSaleItemCommand"/> that defines validation rules for updating a sale item.
    /// </summary>
    /// <remarks>
    /// This validator ensures that the provided values for the sale item update operation 
    /// adhere to business rules and data integrity requirements.
    /// </remarks>
    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleItemCommandValidator"/> class with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - <see cref="UpdateSaleItemCommand.ProductName"/>: Required, cannot exceed 100 characters.
        /// - <see cref="UpdateSaleItemCommand.Quantity"/>: Must be greater than 0 and no more than 20.
        /// - <see cref="UpdateSaleItemCommand.UnitPrice"/>: Must be greater than or equal to 0.
        /// </remarks>
        public UpdateSaleItemCommandValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero.");
        }
    }
}

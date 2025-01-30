using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for creating a sale.
    /// </summary>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - <c>SaleDate</c>: Must not be empty and cannot be set in the future.
        /// - <c>Branch</c>: Must not be empty and must not exceed 50 characters.
        /// - <c>CustomerId</c>: Must not be empty and must be a valid GUID.
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleDate)
                .NotEmpty().WithMessage("Sale date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(50).WithMessage("Branch name cannot exceed 50 characters.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid Customer ID.");
        }
    }
}

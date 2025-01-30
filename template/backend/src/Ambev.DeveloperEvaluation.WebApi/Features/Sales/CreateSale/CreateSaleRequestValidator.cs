using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
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

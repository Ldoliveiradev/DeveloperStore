using Ambev.DeveloperEvaluation.Application.Sales.GetUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem
{
    public class GetSaleItemValidator : AbstractValidator<GetSaleCommand>
    {
        public GetSaleItemValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
}

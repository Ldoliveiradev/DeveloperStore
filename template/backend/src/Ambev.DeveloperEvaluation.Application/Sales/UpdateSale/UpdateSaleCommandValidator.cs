using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Sale ID is required.");

            RuleForEach(x => x.Items)
                .SetValidator(new UpdateSaleItemCommandValidator());
        }
    }
}

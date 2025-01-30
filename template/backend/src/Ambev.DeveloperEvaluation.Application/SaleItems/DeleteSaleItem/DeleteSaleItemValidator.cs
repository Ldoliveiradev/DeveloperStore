using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem
{
    public class DeleteSaleItemValidator : AbstractValidator<DeleteSaleItemCommand>
    {
        public DeleteSaleItemValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
}

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetUser
{
    public class GetSaleValidator : AbstractValidator<GetSaleCommand>
    {
        public GetSaleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
}

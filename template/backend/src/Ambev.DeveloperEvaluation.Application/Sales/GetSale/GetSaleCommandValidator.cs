using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Validator for <see cref="GetSaleCommand"/> that defines validation rules for retrieving a sale.
    /// </summary>
    public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleCommandValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - <c>Id</c>: Must be provided and not empty.
        /// </remarks>
        public GetSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required.");
        }
    }
}

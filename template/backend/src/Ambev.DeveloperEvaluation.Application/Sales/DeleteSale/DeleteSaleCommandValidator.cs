using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Validator for <see cref="DeleteSaleCommand"/> that defines validation rules for deleting a sale.
    /// </summary>
    public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleCommandValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - <c>Id</c>: Must be provided and not empty.
        /// </remarks>
        public DeleteSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required.");
        }
    }
}

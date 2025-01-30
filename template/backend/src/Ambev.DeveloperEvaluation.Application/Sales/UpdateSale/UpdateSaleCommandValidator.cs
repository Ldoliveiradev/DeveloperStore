using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for updating a sale.
    /// </summary>
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleCommandValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - <c>Id</c>: Must be provided and not empty.
        /// - <c>Items</c>: Each item in the list must be validated using <see cref="UpdateSaleItemCommandValidator"/>.
        /// </remarks>
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.");

            RuleForEach(x => x.Items)
                .SetValidator(new UpdateSaleItemCommandValidator());
        }
    }
}

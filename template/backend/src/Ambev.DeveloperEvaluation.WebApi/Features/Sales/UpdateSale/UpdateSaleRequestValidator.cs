using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for <see cref="UpdateSaleRequest"/> that defines validation rules for updating a sale.
    /// </summary>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleRequestValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Required and must be a valid GUID
        /// - Items: Must contain at least one item
        /// - Each Item: Validated using <see cref="UpdateSaleItemRequestValidator"/>
        /// </remarks>
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid Sale ID.");

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Sale must have items.")
                .Must(items => items.Any()).WithMessage("Sale must contain at least one item.");

            RuleForEach(x => x.Items)
                .SetValidator(new UpdateSaleItemRequestValidator());
        }
    }
}

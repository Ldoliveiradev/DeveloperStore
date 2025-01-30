using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.DeleteSaleItem
{
    /// <summary>
    /// Validator for <see cref="DeleteSaleItemRequest"/> to ensure valid request parameters for deleting a sale item.
    /// </summary>
    public class DeleteSaleItemRequestValidator : AbstractValidator<DeleteSaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleItemRequestValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must be provided and not be an empty GUID.
        /// </remarks>
        public DeleteSaleItemRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale Item ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid Sale Item ID.");
        }
    }
}

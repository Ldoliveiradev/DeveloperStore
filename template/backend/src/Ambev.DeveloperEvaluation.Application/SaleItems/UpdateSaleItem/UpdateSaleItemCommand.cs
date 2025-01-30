using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Command for updating a sale item.
    /// </summary>
    /// <remarks>
    /// This command is used to update the details of an existing sale item, 
    /// including product name, quantity, unit price, and cancellation status.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns an <see cref="UpdateSaleItemResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="UpdateSaleItemCommandValidator"/> to ensure that the fields 
    /// are correctly populated and follow the required rules.
    /// </remarks>
    public class UpdateSaleItemCommand : IRequest<UpdateSaleItemResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale item is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleItemCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <param name="isCancelled">Indicates whether the sale item is cancelled.</param>
        public UpdateSaleItemCommand(Guid id, string productName, int quantity, decimal unitPrice, bool isCancelled)
        {
            Id = id;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            IsCancelled = isCancelled;
        }

        /// <summary>
        /// Validates the command using <see cref="UpdateSaleItemCommandValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed.
        /// - Errors: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleItemCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Command for creating a new sale item.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the necessary details for adding a product to a sale, 
    /// including product name, quantity, and unit price.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateSaleItemResult"/>.
    /// </remarks>
    public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product being sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleItemCommand"/> class.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        public CreateSaleItemCommand(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        /// <summary>
        /// Performs validation for the <see cref="CreateSaleItemCommand"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <c>IsValid</c>: Indicates whether all validation rules passed.
        /// - <c>Errors</c>: Collection of validation errors if any rules failed.
        /// </returns>
        /// <remarks>
        /// Validation includes:
        /// <list type="bullet">
        /// <item><description>Product name must not be empty and should be within allowed length.</description></item>
        /// <item><description>Quantity must be greater than 0 and not exceed the allowed limit.</description></item>
        /// <item><description>Unit price must be a positive value.</description></item>
        /// </list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleItemCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

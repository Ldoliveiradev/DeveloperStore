using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a sale, 
    /// including the sale date, branch, customer ID, and a list of sale items. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateSaleResult"/>.
    ///
    /// The data provided in this command is validated using the 
    /// <see cref="CreateSaleCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly populated.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets the date of the sale.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets the branch where the sale was made.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets the unique identifier of the customer associated with the sale.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets the list of items associated with the sale.
        /// </summary>
        public List<CreateSaleItemCommand> Items { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleCommand"/> class.
        /// </summary>
        /// <param name="saleDate">The date when the sale occurred.</param>
        /// <param name="branch">The branch where the sale was made.</param>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <param name="items">The list of items included in the sale.</param>
        public CreateSaleCommand(DateTime saleDate, string branch, Guid customerId, List<CreateSaleItemCommand> items)
        {
            SaleDate = saleDate;
            Branch = branch;
            CustomerId = customerId;
            Items = items;
        }

        /// <summary>
        /// Performs validation of the command using <see cref="CreateSaleCommandValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <see cref="ValidationResultDetail.IsValid"/>: Indicates whether the command is valid.
        /// - <see cref="ValidationResultDetail.Errors"/>: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

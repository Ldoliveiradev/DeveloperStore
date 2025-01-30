using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command for updating an existing sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the necessary details for updating a sale, 
    /// including whether the sale is cancelled and the list of items within the sale. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns an <see cref="UpdateSaleResult"/>.
    ///
    /// The data provided in this command is validated using the 
    /// <see cref="UpdateSaleCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields meet business rules.
    /// </remarks>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the sale to be updated.
        /// </summary>
        public List<UpdateSaleItemCommand> Items { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <param name="isCancelled">Indicates whether the sale is cancelled.</param>
        /// <param name="items">The list of items in the sale to be updated.</param>
        public UpdateSaleCommand(Guid id, bool isCancelled, List<UpdateSaleItemCommand> items)
        {
            Id = id;
            IsCancelled = isCancelled;
            Items = items;
        }

        /// <summary>
        /// Performs validation of the sale update command using <see cref="UpdateSaleCommandValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <see cref="ValidationResultDetail.IsValid"/>: Indicates whether the command is valid.
        /// - <see cref="ValidationResultDetail.Errors"/>: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

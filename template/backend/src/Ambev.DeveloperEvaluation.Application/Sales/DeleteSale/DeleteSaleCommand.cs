using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command for deleting a sale by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This command is used to delete a sale record from the system.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="DeleteSaleResponse"/>.
    ///
    /// The data provided in this command is validated using the 
    /// <see cref="DeleteSaleCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the ID is valid.
    /// </remarks>
    public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        /// <summary>
        /// Gets the unique identifier of the sale to delete.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Performs validation of the command using <see cref="DeleteSaleCommandValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <see cref="ValidationResultDetail.IsValid"/>: Indicates whether the command is valid.
        /// - <see cref="ValidationResultDetail.Errors"/>: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new DeleteSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

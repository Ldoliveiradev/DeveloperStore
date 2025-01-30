using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Command for retrieving a sale by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This command is used to fetch a sale record based on the provided sale ID.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="GetSaleResult"/>.
    ///
    /// The data provided in this command is validated using the 
    /// <see cref="GetSaleCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the ID is valid.
    /// </remarks>
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to retrieve.</param>
        public GetSaleCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Performs validation of the command using <see cref="GetSaleCommandValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <see cref="ValidationResultDetail.IsValid"/>: Indicates whether the command is valid.
        /// - <see cref="ValidationResultDetail.Errors"/>: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new GetSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

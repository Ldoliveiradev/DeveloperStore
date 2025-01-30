using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handles the <see cref="DeleteSaleCommand"/> request to delete a sale by its ID.
    /// </summary>
    /// <remarks>
    /// This handler validates the request using <see cref="DeleteSaleCommandValidator"/>,
    /// logs key actions, and deletes the sale from the repository.
    /// </remarks>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<DeleteSaleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository for sale operations.</param>
        /// <param name="logger">The logger for tracking events.</param>
        public DeleteSaleHandler(ISaleRepository saleRepository, ILogger<DeleteSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the <see cref="DeleteSaleCommand"/> request.
        /// </summary>
        /// <param name="request">The delete sale command containing the sale ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="DeleteSaleResponse"/> indicating success.</returns>
        /// <exception cref="ValidationException">Thrown when the request validation fails.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the sale does not exist.</exception>
        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received request to delete Sale with ID {SaleId} at {Timestamp}",
                request.Id, DateTime.UtcNow);

            var validator = new DeleteSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for DeleteSaleCommand: {Errors}",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new ValidationException(validationResult.Errors);
            }

            _logger.LogInformation("Attempting to delete Sale with ID {SaleId}", request.Id);

            var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
            {
                _logger.LogError("Sale with ID {SaleId} not found", request.Id);
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
            }

            _logger.LogInformation("Successfully deleted Sale with ID {SaleId}", request.Id);

            return new DeleteSaleResponse { Success = true };
        }
    }
}

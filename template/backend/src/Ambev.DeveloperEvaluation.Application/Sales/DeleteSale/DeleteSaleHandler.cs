using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handler for processing DeleteSaleCommand requests.
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<DeleteSaleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="logger">The logger instance.</param>
        public DeleteSaleHandler(ISaleRepository saleRepository, ILogger<DeleteSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the DeleteSaleCommand request.
        /// </summary>
        /// <param name="request">The DeleteSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A response indicating whether the deletion was successful.</returns>
        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received request to delete Sale with ID {SaleId}", request.Id);

            await ValidateCommandAsync(request, cancellationToken);

            await DeleteExistingSaleAsync(request.Id, cancellationToken);

            _logger.LogInformation("Successfully deleted Sale with ID {SaleId}", request.Id);

            return new DeleteSaleResponse { Success = true };
        }

        /// <summary>
        /// Validates the DeleteSaleCommand request.
        /// </summary>
        /// <param name="request">The DeleteSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <exception cref="ValidationException">Thrown if validation fails.</exception>
        private async Task ValidateCommandAsync(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("Validation failed: {Errors}", errors);
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <summary>
        /// Deletes the sale if it exists.
        /// </summary>
        /// <param name="saleId">The ID of the sale to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <exception cref="KeyNotFoundException">Thrown if the sale is not found.</exception>
        private async Task DeleteExistingSaleAsync(Guid saleId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete Sale with ID {SaleId}", saleId);

            var success = await _saleRepository.DeleteAsync(saleId, cancellationToken);
            if (!success)
            {
                _logger.LogError("Sale with ID {SaleId} not found", saleId);
                throw new KeyNotFoundException($"Sale with ID {saleId} not found");
            }
        }
    }
}

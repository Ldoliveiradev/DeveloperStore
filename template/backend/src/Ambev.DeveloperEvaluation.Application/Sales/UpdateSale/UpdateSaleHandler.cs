using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Common.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing UpdateSaleCommand requests.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="eventDispatcher">The event dispatcher for publishing events.</param>
        public UpdateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            ILogger<UpdateSaleHandler> logger,
            IEventDispatcher eventDispatcher)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
            _eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Handles the UpdateSaleCommand request.
        /// </summary>
        /// <param name="command">The UpdateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated sale details.</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing UpdateSaleCommand for SaleId: {SaleId}", command.Id);

            await ValidateCommandAsync(command, cancellationToken);

            var existingSale = await GetExistingSaleAsync(command.Id, cancellationToken);

            _logger.LogInformation("Updating Sale with ID {SaleId}", command.Id);

            UpdateSaleEntity(existingSale, command);

            var updatedSale = await _saleRepository.UpdateAsync(existingSale, cancellationToken);
            _logger.LogInformation("Sale with ID {SaleId} updated successfully", command.Id);

            return _mapper.Map<UpdateSaleResult>(updatedSale);
        }

        /// <summary>
        /// Validates the UpdateSaleCommand request.
        /// </summary>
        /// <param name="command">The UpdateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <exception cref="ValidationException">Thrown if validation fails.</exception>
        private async Task ValidateCommandAsync(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("Validation failed: {Errors}", errors);
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <summary>
        /// Retrieves an existing sale by ID.
        /// </summary>
        /// <param name="saleId">The ID of the sale to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The existing sale entity.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the sale is not found.</exception>
        private async Task<Domain.Entities.Sale> GetExistingSaleAsync(Guid saleId, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);
            if (sale == null)
            {
                _logger.LogError("Sale with ID {SaleId} not found", saleId);
                throw new InvalidOperationException($"Sale with ID {saleId} doesn't exist");
            }
            return sale;
        }

        /// <summary>
        /// Updates an existing sale entity with the provided command details.
        /// </summary>
        /// <param name="existingSale">The existing sale entity.</param>
        /// <param name="command">The UpdateSale command containing updated values.</param>
        private void UpdateSaleEntity(Domain.Entities.Sale existingSale, UpdateSaleCommand command)
        {
            if (command.IsCancelled)
            {
                _logger.LogInformation("Cancelling Sale ID {SaleId}", command.Id);
                existingSale.Cancel();
                _eventDispatcher.Publish(new SaleCancelledEvent(existingSale.Id));
            }

            foreach (var item in command.Items)
            {
                _logger.LogInformation("Updating Sale Item: {ProductName} (Quantity: {Quantity}, Price: {UnitPrice}, IsCancelled: {IsCancelled})",
                    item.ProductName, item.Quantity, item.UnitPrice, item.IsCancelled);

                var existingItem = existingSale.SaleItems.FirstOrDefault(x => x.Id == item.Id);

                if (item.IsCancelled && existingItem != null && !existingItem.IsCancelled)
                {
                    _eventDispatcher.Publish(new ItemCancelledEvent(item.Id, existingItem.Id, item.Quantity));
                }

                existingSale.UpdateProduct(item.ProductName, item.Quantity, item.UnitPrice, item.IsCancelled);
            }
        }
    }
}

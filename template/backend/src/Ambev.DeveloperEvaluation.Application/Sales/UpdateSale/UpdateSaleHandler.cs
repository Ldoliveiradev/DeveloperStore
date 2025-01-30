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
    /// Handles the <see cref="UpdateSaleCommand"/> request to update an existing sale.
    /// </summary>
    /// <remarks>
    /// This handler validates and processes an update to a sale, including:
    /// - Modifying sale items.
    /// - Handling sale cancellations.
    /// - Dispatching events for cancellations.
    /// 
    /// The <see cref="UpdateSaleCommandValidator"/> ensures that the provided data meets 
    /// the necessary validation rules before processing.
    /// 
    /// The handler logs all major events, including validation failures, missing sales, updates, 
    /// and cancellations using <see cref="ILogger{UpdateSaleHandler}"/>.
    /// 
    /// Additionally, it utilizes <see cref="IEventDispatcher"/> to publish events related to 
    /// sale and item cancellations.
    /// </remarks>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository for sale data operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping data.</param>
        /// <param name="logger">The logger for tracking events.</param>
        /// <param name="eventDispatcher">The event dispatcher for publishing domain events.</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<UpdateSaleHandler> logger, IEventDispatcher eventDispatcher)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
            _eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Processes the <see cref="UpdateSaleCommand"/> request.
        /// </summary>
        /// <param name="command">The update request containing sale modification details.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>The updated sale details.</returns>
        /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the sale does not exist.</exception>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing UpdateSaleCommand for SaleId: {SaleId} at {Timestamp}",
                command.Id, DateTime.UtcNow);

            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for UpdateSaleCommand: {Errors}",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new ValidationException(validationResult.Errors);
            }

            var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingSale == null)
            {
                _logger.LogError("Sale with ID {SaleId} not found", command.Id);
                throw new InvalidOperationException($"Sale with id {command.Id} doesn't exist");
            }

            _logger.LogInformation("Updating Sale with ID {SaleId}", command.Id);

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

            var sale = await _saleRepository.UpdateAsync(existingSale, cancellationToken);
            _logger.LogInformation("Sale with ID {SaleId} updated successfully", command.Id);

            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
}

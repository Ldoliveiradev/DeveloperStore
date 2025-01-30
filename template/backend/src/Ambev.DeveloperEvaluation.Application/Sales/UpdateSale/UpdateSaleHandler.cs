using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Common.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;

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

        private async Task<Domain.Entities.Sale> GetExistingSaleAsync(Guid saleId, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);
            if (sale == null)
            {
                _logger.LogError("Sale with ID {SaleId} not found", saleId);
                throw new InvalidOperationException($"Sale with id {saleId} doesn't exist");
            }
            return sale;
        }

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

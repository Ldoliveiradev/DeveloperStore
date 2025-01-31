using Ambev.DeveloperEvaluation.Common.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="eventDispatcher">The event dispatcher for publishing events.</param>
        public CreateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            ILogger<CreateSaleHandler> logger,
            IEventDispatcher eventDispatcher)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
            _eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale details.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing CreateSaleCommand for CustomerId: {CustomerId}", command.CustomerId);

            await ValidateCommandAsync(command, cancellationToken);

            var newSale = CreateSaleEntity(command);

            _logger.LogInformation("Creating sale for CustomerId: {CustomerId} with {ItemCount} items",
                command.CustomerId, command.Items.Count);

            var createdSale = await _saleRepository.CreateAsync(newSale, cancellationToken);

            _logger.LogInformation("Sale created successfully with ID: {SaleId}", createdSale.Id);

            _eventDispatcher.Publish(new SaleCreatedEvent(createdSale.Id));

            return _mapper.Map<CreateSaleResult>(createdSale);
        }

        /// <summary>
        /// Validates the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <exception cref="ValidationException">Thrown if validation fails.</exception>
        private async Task ValidateCommandAsync(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("Validation failed: {Errors}", errors);
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <summary>
        /// Creates a new Sale entity from the given CreateSaleCommand.
        /// </summary>
        /// <param name="command">The CreateSale command.</param>
        /// <returns>The newly created Sale entity.</returns>
        private Sale CreateSaleEntity(CreateSaleCommand command)
        {
            var sale = _mapper.Map<Sale>(command);
            foreach (var item in command.Items)
            {
                sale.AddProduct(item.ProductName, item.Quantity, item.UnitPrice);
            }
            return sale;
        }
    }
}

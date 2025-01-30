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
    /// Handles the <see cref="CreateSaleCommand"/> request to create a new sale.
    /// </summary>
    /// <remarks>
    /// This handler performs the following operations:
    /// - Validates the incoming request using <see cref="CreateSaleCommandValidator"/>.
    /// - Logs major processing steps and validation failures using <see cref="ILogger{CreateSaleHandler}"/>.
    /// - Creates a new sale with associated sale items.
    /// - Dispatches the <see cref="SaleCreatedEvent"/> upon successful creation.
    /// </remarks>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository for sale data operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping data.</param>
        /// <param name="logger">The logger for tracking events.</param>
        /// <param name="eventDispatcher">The event dispatcher for publishing domain events.</param>
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
        /// Processes the <see cref="CreateSaleCommand"/> request.
        /// </summary>
        /// <param name="command">The request containing sale creation details.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>A <see cref="CreateSaleResult"/> containing details of the newly created sale.</returns>
        /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing CreateSaleCommand for CustomerId: {CustomerId} at {Timestamp}",
                command.CustomerId, DateTime.UtcNow);

            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for CreateSaleCommand: {Errors}",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new ValidationException(validationResult.Errors);
            }

            var newSale = _mapper.Map<Sale>(command);

            foreach (var item in command.Items)
            {
                newSale.AddProduct(item.ProductName, item.Quantity, item.UnitPrice);
            }

            _logger.LogInformation("Creating Sale for CustomerId: {CustomerId} with {ItemCount} items",
                command.CustomerId, command.Items.Count);

            var createdSale = await _saleRepository.CreateAsync(newSale, cancellationToken);

            _logger.LogInformation("Sale created successfully with ID: {SaleId}", createdSale.Id);

            _eventDispatcher.Publish(new SaleCreatedEvent(newSale.Id));

            return _mapper.Map<CreateSaleResult>(createdSale);
        }
    }
}

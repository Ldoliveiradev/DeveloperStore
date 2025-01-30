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
    /// This handler validates the request using <see cref="CreateSaleCommandValidator"/>,
    /// logs key actions, and creates a new sale with associated items in the repository.
    /// </remarks>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository for sale operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping domain objects.</param>
        /// <param name="logger">The logger for tracking events.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<CreateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the <see cref="CreateSaleCommand"/> request.
        /// </summary>
        /// <param name="command">The create sale command containing sale details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="CreateSaleResult"/> containing the created sale details.</returns>
        /// <exception cref="ValidationException">Thrown when the request validation fails.</exception>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateSaleCommand for CustomerId: {CustomerId} at {Timestamp}",
                command.CustomerId, DateTime.UtcNow);

            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for CreateSaleCommand: {Errors}",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new ValidationException(validationResult.Errors);
            }

            var newSale = new Sale(command.SaleDate, command.Branch, command.CustomerId);

            foreach (var item in command.Items)
            {
                newSale.AddProduct(item.ProductName, item.Quantity, item.UnitPrice);
            }

            _logger.LogInformation("Creating Sale for CustomerId: {CustomerId} with {ItemCount} items",
                command.CustomerId, command.Items.Count);

            var createdSale = await _saleRepository.CreateAsync(newSale, cancellationToken);

            _logger.LogInformation("Sale created successfully with ID: {SaleId}", createdSale.Id);

            return _mapper.Map<CreateSaleResult>(createdSale);
        }
    }
}

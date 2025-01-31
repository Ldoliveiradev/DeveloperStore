using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Handler for processing GetSaleCommand requests.
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger instance.</param>
        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the GetSaleCommand request.
        /// </summary>
        /// <param name="request">The GetSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The retrieved sale details.</returns>
        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetSaleCommand for SaleId: {SaleId}", request.Id);

            await ValidateCommandAsync(request, cancellationToken);

            var sale = await GetExistingSaleAsync(request.Id, cancellationToken);

            _logger.LogInformation("Sale with ID {SaleId} retrieved successfully", request.Id);

            return _mapper.Map<GetSaleResult>(sale);
        }

        /// <summary>
        /// Validates the GetSaleCommand request.
        /// </summary>
        /// <param name="request">The GetSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <exception cref="ValidationException">Thrown if validation fails.</exception>
        private async Task ValidateCommandAsync(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

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
        /// <returns>The sale entity.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the sale is not found.</exception>
        private async Task<Domain.Entities.Sale> GetExistingSaleAsync(Guid saleId, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);
            if (sale == null)
            {
                _logger.LogError("Sale with ID {SaleId} not found", saleId);
                throw new KeyNotFoundException($"Sale with ID {saleId} not found");
            }
            return sale;
        }
    }
}

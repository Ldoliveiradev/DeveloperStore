using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Handles the GetSaleCommand request to retrieve a sale by its ID.
    /// </summary>
    /// <remarks>
    /// This handler retrieves a sale from the repository based on the provided ID.
    /// It validates the request using <see cref="GetSaleCommandValidator"/> 
    /// and logs significant processing steps using <see cref="ILogger{GetSaleHandler}"/>.
    /// </remarks>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository for sale data operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping data.</param>
        /// <param name="logger">The logger for tracking events.</param>
        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the GetSaleCommand request.
        /// </summary>
        /// <param name="request">The GetSale command containing the sale ID.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>The sale details if found.</returns>
        /// <exception cref="ValidationException">Thrown when the request validation fails.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the sale is not found.</exception>
        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetSaleCommand for SaleId: {SaleId} at {Timestamp}",
                request.Id, DateTime.UtcNow);

            var validator = new GetSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for GetSaleCommand: {Errors}",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new ValidationException(validationResult.Errors);
            }

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
            {
                _logger.LogError("Sale with ID {SaleId} not found", request.Id);
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
            }

            _logger.LogInformation("Sale with ID {SaleId} retrieved successfully", request.Id);

            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}

using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleHandler> _logger;

        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetSaleCommand for SaleId: {SaleId}", request.Id);

            await ValidateCommandAsync(request, cancellationToken);

            var sale = await GetExistingSaleAsync(request.Id, cancellationToken);

            _logger.LogInformation("Sale with ID {SaleId} retrieved successfully", request.Id);

            return _mapper.Map<GetSaleResult>(sale);
        }

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

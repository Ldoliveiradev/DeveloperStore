using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
    {
        readonly ISaleItemRepository _saleItemRepository;
        readonly IMapper _mapper;
        readonly ISaleRepository _saleRepository;

        public CreateSaleItemHandler(ISaleItemRepository saleItemRepository, IMapper mapper, ISaleRepository saleRepository)
        {
            _saleItemRepository = saleItemRepository;
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleItemCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            //var existingSale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
            //if (existingSale is null)
            //    throw new InvalidOperationException($"Sale with saleId {command.SaleId} doesn't exist");

            //existingSale.AddProduct(command.ProductName, command.Quantity, command.UnitPrice);

            //var saleItem = _mapper.Map<SaleItem>(command);

            ////var createdSaleItem = await _saleItemRepository.CreateAsync(saleItem, cancellationToken);
            //var createdSaleItem = await _saleRepository.CreateItemAsync(existingSale, cancellationToken);
            //var result = _mapper.Map<CreateSaleItemResult>(createdSaleItem.SaleItems.LastOrDefault());
            //return result;

            return new CreateSaleItemResult();
        }
    }
}

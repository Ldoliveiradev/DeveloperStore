using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        readonly ISaleRepository _saleRepository;
        readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetByIdAsync(command.Id);
            if (existingSale == null)
                throw new InvalidOperationException($"Sale with id {command.Id} doesn't exist");

            if (command.IsCancelled)
                existingSale.Cancel();

            foreach (var item in command.Items)
            {
                existingSale.UpdateProduct(item.ProductName, item.Quantity, item.UnitPrice, item.IsCancelled);
            }

            await _saleRepository.UpdateAsync(existingSale, cancellationToken);
            return _mapper.Map<UpdateSaleResult>(existingSale);
        }
    }
}

using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem
{
    public class DeleteSaleItemHandler : IRequestHandler<DeleteSaleItemCommand, DeleteSaleItemResponse>
    {
        readonly ISaleItemRepository _saleItemRepository;

        public DeleteSaleItemHandler(ISaleItemRepository saleItemRepository)
        {
            _saleItemRepository = saleItemRepository;
        }

        public async Task<DeleteSaleItemResponse> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleItemValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _saleItemRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"Sale item with ID {request.Id} not found");

            return new DeleteSaleItemResponse { Success = true };
        }
    }
}

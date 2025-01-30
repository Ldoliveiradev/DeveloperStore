using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem
{
    public class DeleteSaleItemCommand : IRequest<DeleteSaleItemResponse>
    {
        public Guid Id { get; }

        public DeleteSaleItemCommand(Guid id)
        {
            Id = id;
        }
    }
}

using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }
        public bool IsCancelled { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; } = new();

        public UpdateSaleCommand(Guid id, bool isCancelled, List<UpdateSaleItemCommand> items)
        {
            Id = id;
            IsCancelled = isCancelled;
            Items = items;
        }
    }
}

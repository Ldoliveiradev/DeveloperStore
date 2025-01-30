using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem
{
    public class GetSaleItemCommand : IRequest<GetSaleItemResult>
    {
        public Guid Id { get; }

        public GetSaleItemCommand(Guid id)
        {
            Id = id;
        }
    }
}

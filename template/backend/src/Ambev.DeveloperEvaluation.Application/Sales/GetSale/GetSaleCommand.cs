using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetUser
{
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        public Guid Id { get; }

        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}

using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public Guid Id { get; set; }
        public bool IsCancelled { get; set; }
        public IEnumerable<UpdateSaleItemRequest> Items { get; set; }
    }
}

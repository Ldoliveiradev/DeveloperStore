using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public DateTime SaleDate { get; set; }
        public string Branch { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<CreateSaleItemRequest> Items { get; set; }
    }
}

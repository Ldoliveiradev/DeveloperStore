using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }
        public Guid CustomerId { get; set; }

        public IEnumerable<GetSaleItemResponse> SaleItems { get; set; }
    }
}

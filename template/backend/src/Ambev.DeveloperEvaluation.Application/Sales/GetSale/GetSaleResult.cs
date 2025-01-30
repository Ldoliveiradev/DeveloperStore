using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }
        public Guid CustomerId { get; set; }

        public IEnumerable<GetSaleItemResult> SaleItems { get; set; }
    }
}

using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public Guid Id { get; set; }
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public bool IsCancelled { get; set; }
        public Guid CustomerId { get; set; }

        public List<UpdateSaleItemResponse> Items { get; set; } = [];
    }
}

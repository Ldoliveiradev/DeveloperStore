namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    public class UpdateSaleItemResult
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid SaleId { get; set; }
    }
}

namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    public class ItemCancelledEvent
    {
        public Guid SaleId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }
        public DateTime CancelledAt { get; }

        public ItemCancelledEvent(Guid saleId, Guid productId, int quantity)
        {
            SaleId = saleId;
            ProductId = productId;
            Quantity = quantity;
            CancelledAt = DateTime.UtcNow;
        }
    }
}

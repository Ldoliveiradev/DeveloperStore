namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class SaleCancelledEvent
    {
        public Guid SaleId { get; }
        public DateTime CancelledAt { get; }

        public SaleCancelledEvent(Guid saleId)
        {
            SaleId = saleId;
            CancelledAt = DateTime.UtcNow;
        }
    }
}

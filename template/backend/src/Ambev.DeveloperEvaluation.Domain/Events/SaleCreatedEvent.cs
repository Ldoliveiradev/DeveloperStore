namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; }
        public DateTime CreatedAt { get; }

        public SaleCreatedEvent(Guid saleId)
        {
            SaleId = saleId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

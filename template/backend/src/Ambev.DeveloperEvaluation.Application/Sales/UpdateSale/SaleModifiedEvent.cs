namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class SaleModifiedEvent
    {
        public Guid SaleId { get; }
        public DateTime ModifiedAt { get; }

        public SaleModifiedEvent(Guid saleId)
        {
            SaleId = saleId;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}

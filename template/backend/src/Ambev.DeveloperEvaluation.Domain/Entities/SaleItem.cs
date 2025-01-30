using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
        public bool IsCancelled { get; private set; }
        public Guid SaleId { get; private set; }

        public Sale Sale { get; private set; }

        public SaleItem(Guid saleId, string productName, int quantity, decimal unitPrice, decimal discount)
        {
            SaleId = saleId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;

            CalculateTotalAmount();
        }

        public SaleItem(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            CalculateTotalAmount();
        }

        public void UpdateQuantityAndPrice(int newQuantity, decimal newUnitPrice)
        {
            if (newQuantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            CalculateTotalAmount();

            Quantity = newQuantity;
            UnitPrice = newUnitPrice;
            Discount = Sale.CalculateDiscount(newQuantity, newUnitPrice);
        }

        public void Cancel()
        {
            IsCancelled = true;
            TotalAmount = 0;
            Quantity = 0;
            UnitPrice = 0;
            Discount = 0;
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = (UnitPrice * Quantity) - Discount;
        }
    }
}

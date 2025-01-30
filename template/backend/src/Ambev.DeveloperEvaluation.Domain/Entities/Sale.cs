using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public int SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Branch { get; private set; }
        public bool IsCancelled { get; private set; }
        public Guid CustomerId { get; private set; }

        public User Customer { get; private set; } = new();

        private readonly List<SaleItem> _saleItems = [];
        public IReadOnlyCollection<SaleItem> SaleItems => _saleItems.AsReadOnly();

        public Sale(DateTime saleDate, string branch, Guid customerId)
        {
            Id = Guid.NewGuid();
            SaleDate = saleDate;
            CustomerId = customerId;
            Branch = branch;
            IsCancelled = false;
            TotalAmount = 0;
        }

        public void AddProduct(string productName, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            var discount = CalculateDiscount(quantity, unitPrice);

            if (quantity < 4 && discount > 0)
                throw new InvalidOperationException("Cannot apply discounts for less than 4 items.");

            var saleItem = new SaleItem(Id, productName, quantity, unitPrice, discount);
            _saleItems.Add(saleItem);
            RecalculateTotalAmount();
        }

        public void UpdateProduct(string productName, int quantity, decimal unitPrice, bool isCancelled)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            var discount = CalculateDiscount(quantity, unitPrice);

            if (quantity < 4 && discount > 0)
                throw new InvalidOperationException("Cannot apply discounts for less than 4 items.");

            var existingItem = _saleItems.FirstOrDefault(i => i.ProductName == productName);

            if (existingItem != null)
            {
                existingItem.UpdateQuantityAndPrice(quantity, unitPrice);

                if (isCancelled)
                    existingItem.Cancel();
            }
            else
            {
                var newSaleItem = new SaleItem(Id, productName, quantity, unitPrice, discount);
                _saleItems.Add(newSaleItem);
            }

            RecalculateTotalAmount();
        }
        public void Cancel()
        {
            IsCancelled = true;

            foreach (var item in _saleItems)
            {
                item.Cancel();
            }

            TotalAmount = 0;
        }

        private void RecalculateTotalAmount()
        {
            TotalAmount = _saleItems.Sum(item => item.TotalAmount);
        }
    }
}

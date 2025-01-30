using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction within the system.
    /// A sale contains details such as sale date, total amount, branch, cancellation status, 
    /// and associated customer and sale items.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the sale number, uniquely identifying the sale.
        /// </summary>
        public int SaleNumber { get; private set; }

        /// <summary>
        /// Gets the date when the sale was created.
        /// </summary>
        public DateTime SaleDate { get; private set; }

        /// <summary>
        /// Gets the total amount of the sale.
        /// This is automatically calculated based on the added sale items.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets the branch where the sale was processed.
        /// </summary>
        public string Branch { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the sale has been canceled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the customer who made the purchase.
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Gets the customer associated with the sale.
        /// </summary>
        public User Customer { get; private set; } = new();

        /// <summary>
        /// List of sale items included in this sale.
        /// </summary>
        private readonly List<SaleItem> _saleItems = [];

        /// <summary>
        /// Gets a read-only collection of sale items.
        /// </summary>
        public IReadOnlyCollection<SaleItem> SaleItems => _saleItems.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="Sale"/> class.
        /// </summary>
        /// <param name="saleDate">The date of the sale.</param>
        /// <param name="branch">The branch where the sale occurred.</param>
        /// <param name="customerId">The ID of the customer.</param>
        public Sale(DateTime saleDate, string branch, Guid customerId)
        {
            Id = Guid.NewGuid();
            SaleDate = saleDate;
            CustomerId = customerId;
            Branch = branch;
            IsCancelled = false;
            TotalAmount = 0;
        }

        /// <summary>
        /// Adds a product to the sale.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product being purchased.</param>
        /// <param name="unitPrice">The price per unit of the product.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the quantity is greater than 20 or if discounts are applied incorrectly.
        /// </exception>
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

        /// <summary>
        /// Updates an existing product in the sale or adds a new one if it does not exist.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The updated quantity of the product.</param>
        /// <param name="unitPrice">The updated price per unit of the product.</param>
        /// <param name="isCancelled">Indicates if the product should be marked as canceled.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the quantity is greater than 20 or if discounts are applied incorrectly.
        /// </exception>
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

        /// <summary>
        /// Cancels the entire sale, marking all products as canceled.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;

            foreach (var item in _saleItems)
            {
                item.Cancel();
            }

            TotalAmount = 0;
        }

        /// <summary>
        /// Recalculates the total amount of the sale based on the current sale items.
        /// </summary>
        private void RecalculateTotalAmount()
        {
            TotalAmount = _saleItems.Sum(item => item.TotalAmount);
        }
    }
}

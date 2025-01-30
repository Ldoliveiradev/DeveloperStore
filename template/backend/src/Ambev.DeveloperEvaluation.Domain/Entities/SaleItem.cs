using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale transaction.
    /// Each sale item contains details about the product, its quantity, unit price, discount, and total amount.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the name of the product in the sale item.
        /// </summary>
        public string ProductName { get; private set; }

        /// <summary>
        /// Gets the quantity of the product being purchased.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the price per unit of the product.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the discount applied to the product.
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets the total amount for the sale item after applying the discount.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the sale item has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the sale that this item belongs to.
        /// </summary>
        public Guid SaleId { get; private set; }

        /// <summary>
        /// Gets the sale associated with this sale item.
        /// </summary>
        public Sale Sale { get; private set; } = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class.
        /// </summary>
        /// <param name="saleId">The unique identifier of the associated sale.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The price per unit of the product.</param>
        /// <param name="discount">The discount applied to the product.</param>
        public SaleItem(Guid saleId, string productName, int quantity, decimal unitPrice, decimal discount)
        {
            SaleId = saleId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;

            CalculateTotalAmount();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class without a sale ID.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The price per unit of the product.</param>
        public SaleItem(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            CalculateTotalAmount();
        }

        /// <summary>
        /// Updates the quantity and price of the sale item.
        /// </summary>
        /// <param name="newQuantity">The new quantity of the product.</param>
        /// <param name="newUnitPrice">The new price per unit of the product.</param>
        /// <exception cref="InvalidOperationException">Thrown if the quantity exceeds 20 items.</exception>
        public void UpdateQuantityAndPrice(int newQuantity, decimal newUnitPrice)
        {
            if (newQuantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            Quantity = newQuantity;
            UnitPrice = newUnitPrice;
            Discount = Sale.CalculateDiscount(newQuantity, newUnitPrice);

            CalculateTotalAmount();
        }

        /// <summary>
        /// Cancels the sale item, setting its quantity and total amount to zero.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
            TotalAmount = 0;
            Quantity = 0;
            UnitPrice = 0;
            Discount = 0;
        }

        /// <summary>
        /// Calculates the total amount for the sale item after applying the discount.
        /// </summary>
        private void CalculateTotalAmount()
        {
            TotalAmount = (UnitPrice * Quantity) - Discount;
        }
    }
}

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Represents the response returned after successfully creating a new sale item.
    /// </summary>
    public class CreateSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product name of the sale item.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the sale item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the sale item.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale item is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}

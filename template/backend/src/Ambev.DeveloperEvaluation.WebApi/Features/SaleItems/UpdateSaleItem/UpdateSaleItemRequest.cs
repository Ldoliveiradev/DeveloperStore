namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Represents a request to update a sale item.
    /// </summary>
    public class UpdateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item to be updated.
        /// </summary>
        /// <value>A GUID representing the sale item ID.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product in the sale item.
        /// </summary>
        /// <value>A string containing the product name.</value>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product in the sale item.
        /// </summary>
        /// <value>An integer representing the quantity.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product in the sale item.
        /// </summary>
        /// <value>A decimal representing the unit price.</value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale item is cancelled.
        /// </summary>
        /// <value>A boolean indicating the cancellation status.</value>
        public bool IsCancelled { get; set; }
    }
}

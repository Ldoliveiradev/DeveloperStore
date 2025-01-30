namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem
{
    /// <summary>
    /// Represents the response containing details of a sale item.
    /// </summary>
    public class GetSaleItemResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        /// <value>A GUID representing the sale item ID.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product in the sale item.
        /// </summary>
        /// <value>A string containing the product name.</value>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        /// <value>An integer representing the quantity.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        /// <value>A decimal representing the unit price.</value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the sale item.
        /// </summary>
        /// <value>A decimal representing the discount amount.</value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the sale item after discounts.
        /// </summary>
        /// <value>A decimal representing the total amount.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale item has been cancelled.
        /// </summary>
        /// <value>A boolean representing the cancellation status.</value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the sale associated with this item.
        /// </summary>
        /// <value>A GUID representing the related sale ID.</value>
        public Guid SaleId { get; set; }
    }
}

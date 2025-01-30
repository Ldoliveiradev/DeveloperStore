namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem
{
    /// <summary>
    /// Represents the response model for retrieving sale item details.
    /// </summary>
    /// <remarks>
    /// This DTO encapsulates the details of a sale item, including pricing, quantity,
    /// discount applied, total amount, and cancellation status.
    /// </remarks>
    public class GetSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the sale item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount after applying discounts.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets whether the sale item is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated sale.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}

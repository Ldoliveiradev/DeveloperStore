namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Represents the result of a successfully updated sale item.
    /// </summary>
    /// <remarks>
    /// This model provides detailed information about the updated sale item, 
    /// including product details, quantity, pricing, and related sale reference.
    /// </remarks>
    public class UpdateSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product associated with this sale item.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product in the updated sale.
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
        /// Gets or sets the total amount for this sale item after applying the discount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated sale.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}

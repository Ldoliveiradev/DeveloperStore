namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Represents the response model returned after successfully updating a sale item.
    /// </summary>
    /// <remarks>
    /// This response provides details of the updated sale item, including its 
    /// ID, product details, pricing, and associated sale ID.
    /// </remarks>
    public class UpdateSaleItemResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product associated with the sale item.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product in the sale.
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
        /// Gets or sets the total amount for this sale item, after applying discounts.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated sale.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}

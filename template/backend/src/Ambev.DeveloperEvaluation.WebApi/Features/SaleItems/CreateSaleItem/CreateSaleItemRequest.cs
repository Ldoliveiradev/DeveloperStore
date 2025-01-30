namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Represents a request to create a new sale item.
    /// </summary>
    public class CreateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the name of the product being sold.
        /// </summary>
        /// <value>The product name as a string.</value>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product in the sale.
        /// </summary>
        /// <value>The number of items sold.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        /// <value>The price per unit of the product.</value>
        public decimal UnitPrice { get; set; }
    }
}

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents the response returned after successfully creating a sale.
    /// </summary>
    public class CreateSaleResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created sale.
        /// </summary>
        /// <value>A GUID representing the sale ID.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        /// <value>An integer representing the sale number.</value>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the date when the sale occurred.
        /// </summary>
        /// <value>A DateTime representing the sale date.</value>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the sale.
        /// </summary>
        /// <value>A decimal representing the total sale amount.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        /// <value>A string representing the branch name.</value>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the sale is cancelled.
        /// </summary>
        /// <value>A boolean indicating the cancellation status.</value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer associated with the sale.
        /// </summary>
        /// <value>A GUID representing the customer ID.</value>
        public Guid CustomerId { get; set; }
    }
}

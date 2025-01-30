namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Represents the response returned after successfully creating a new sale.
    /// </summary>
    /// <remarks>
    /// This response contains detailed information about the newly created sale,
    /// including its unique identifier, sale number, date, total amount, branch,
    /// cancellation status, and associated customer.
    /// </remarks>
    public class CreateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created sale.
        /// </summary>
        /// <value>A GUID that uniquely identifies the sale.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sequential sale number.
        /// </summary>
        /// <value>An integer representing the sale number.</value>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the date when the sale was created.
        /// </summary>
        /// <value>The date and time when the sale occurred.</value>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the sale, including all items and discounts.
        /// </summary>
        /// <value>The total monetary value of the sale.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale was processed.
        /// </summary>
        /// <value>A string representing the branch location.</value>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the sale has been canceled.
        /// </summary>
        /// <value><c>true</c> if the sale is canceled; otherwise, <c>false</c>.</value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer associated with the sale.
        /// </summary>
        /// <value>A GUID that uniquely identifies the customer.</value>
        public Guid CustomerId { get; set; }
    }
}

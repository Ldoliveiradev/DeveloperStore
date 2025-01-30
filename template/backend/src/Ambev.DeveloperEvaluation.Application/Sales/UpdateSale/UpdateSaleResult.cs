using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents the response returned after successfully updating a sale.
    /// </summary>
    /// <remarks>
    /// This response contains the updated details of the sale, including its
    /// unique identifier, sale number, date, total amount, branch, cancellation
    /// status, associated customer, and updated sale items.
    /// </remarks>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated sale.
        /// </summary>
        /// <value>A GUID that uniquely identifies the sale.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sale number, which is a sequential identifier for the sale.
        /// </summary>
        /// <value>An integer representing the sale number.</value>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        /// <value>The date and time when the sale occurred.</value>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the sale, including all items and discounts.
        /// </summary>
        /// <value>The total price of the sale transaction.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        /// <value>A string representing the branch location of the sale.</value>
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

        /// <summary>
        /// Gets or sets the list of sale items associated with the updated sale.
        /// </summary>
        /// <value>A list of <see cref="UpdateSaleItemResponse"/> objects representing the updated sale items.</value>
        public List<UpdateSaleItemResponse> Items { get; set; } = [];
    }
}

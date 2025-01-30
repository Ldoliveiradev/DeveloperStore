namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.DeleteSaleItem
{
    /// <summary>
    /// Represents a request to delete a sale item.
    /// </summary>
    public class DeleteSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item to be deleted.
        /// </summary>
        /// <value>A GUID representing the sale item ID.</value>
        public Guid Id { get; set; }
    }
}

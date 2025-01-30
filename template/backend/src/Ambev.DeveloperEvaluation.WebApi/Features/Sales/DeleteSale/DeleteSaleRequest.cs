namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    /// <summary>
    /// Represents a request to delete an existing sale.
    /// </summary>
    public class DeleteSaleRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to be deleted.
        /// </summary>
        /// <value>A GUID representing the sale ID.</value>
        public Guid Id { get; set; }
    }
}

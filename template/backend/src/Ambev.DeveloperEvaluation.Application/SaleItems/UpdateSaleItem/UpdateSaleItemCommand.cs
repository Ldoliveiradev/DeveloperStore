using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    public class UpdateSaleItemCommand : IRequest<UpdateSaleItemResult>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsCancelled { get; set; }

        public UpdateSaleItemCommand(Guid id, string productName, int quantity, decimal unitPrice, bool isCancelled)
        {
            Id = id;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            IsCancelled = isCancelled;
        }
    }
}

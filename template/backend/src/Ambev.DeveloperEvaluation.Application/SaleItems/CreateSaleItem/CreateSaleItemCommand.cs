using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        //public decimal Discount { get; set; }
        //public decimal TotalAmount { get; set; }
        //public Guid SaleId { get; set; }

        public CreateSaleItemCommand(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            //Discount = discount;
            //TotalAmount = totalAmount;
            //SaleId = saleId;
        }
    }
}

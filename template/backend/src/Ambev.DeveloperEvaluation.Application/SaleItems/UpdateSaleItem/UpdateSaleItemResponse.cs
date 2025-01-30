﻿namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    public class UpdateSaleItemResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid SaleId { get; set; }
    }
}

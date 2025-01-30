﻿namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem
{
    public class GetSaleItemResult
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public Guid SaleId { get; set; }
    }
}

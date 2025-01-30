﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItem
{
    public class UpdateSaleItemRequest
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsCancelled { get; set; }
    }
}

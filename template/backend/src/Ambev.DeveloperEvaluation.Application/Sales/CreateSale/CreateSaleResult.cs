﻿namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }
        public Guid CustomerId { get; set; }
    }
}

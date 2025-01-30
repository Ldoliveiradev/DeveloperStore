using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public DateTime SaleDate { get; set; }
        public string Branch { get; set; }
        public Guid CustomerId { get; set; }
        public List<CreateSaleItemCommand> Items { get; set; } = [];

        public CreateSaleCommand(DateTime saleDate, string branch, Guid customerId, List<CreateSaleItemCommand> items)
        {
            SaleDate = saleDate;
            Branch = branch;
            CustomerId = customerId;
            Items = items;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

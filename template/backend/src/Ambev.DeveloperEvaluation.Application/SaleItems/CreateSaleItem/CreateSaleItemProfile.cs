using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    public class CreateSaleItemProfile : Profile
    {
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemCommand, SaleItem>()
               .ConstructUsing(cmd => new SaleItem(cmd.ProductName, cmd.Quantity, cmd.UnitPrice));

            CreateMap<SaleItem, CreateSaleItemResult>();
        }
    }
}

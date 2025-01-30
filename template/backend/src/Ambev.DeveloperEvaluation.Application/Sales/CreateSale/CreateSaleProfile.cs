using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
               .ConstructUsing(cmd => new Sale(cmd.SaleDate, cmd.Branch, cmd.CustomerId));

            CreateMap<Sale, CreateSaleResult>();
        }
    }
}

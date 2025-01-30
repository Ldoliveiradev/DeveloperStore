using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        }
    }
}

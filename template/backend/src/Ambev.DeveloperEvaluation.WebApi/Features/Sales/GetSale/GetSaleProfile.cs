using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Guid, GetSaleCommand>()
                .ConstructUsing(id => new GetSaleCommand(id));

            CreateMap<Sale, GetSaleResult>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

            CreateMap<SaleItem, GetSaleItemResult>();

            CreateMap<GetSaleResult, GetSaleResponse>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

            CreateMap<GetSaleItemResult, GetSaleItemResponse>();
        }
    }
}

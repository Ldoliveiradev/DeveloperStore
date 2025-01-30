using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between API and Application GetSale requests and responses.
    /// </summary>
    public class GetSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the GetSale feature.
        /// </summary>
        public GetSaleProfile()
        {
            // Maps a Guid (ID) to GetSaleCommand for retrieval
            CreateMap<Guid, GetSaleCommand>()
                .ConstructUsing(id => new GetSaleCommand(id));

            // Maps Sale entity to GetSaleResult, including its SaleItems
            CreateMap<Sale, GetSaleResult>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

            // Maps SaleItem entity to GetSaleItemResult
            CreateMap<SaleItem, GetSaleItemResult>();

            // Maps GetSaleResult to GetSaleResponse, including its SaleItems
            CreateMap<GetSaleResult, GetSaleResponse>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

            // Maps GetSaleItemResult to GetSaleItemResponse
            CreateMap<GetSaleItemResult, GetSaleItemResponse>();
        }
    }
}

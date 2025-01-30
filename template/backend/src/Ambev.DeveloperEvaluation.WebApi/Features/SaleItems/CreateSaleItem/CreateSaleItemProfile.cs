using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem
{
    public class CreateSaleItemProfile : Profile
    {
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
            CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
        }
    }
}

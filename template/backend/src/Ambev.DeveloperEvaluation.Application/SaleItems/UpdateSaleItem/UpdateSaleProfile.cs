using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// AutoMapper profile for mapping between domain models and application DTOs related to sale item updates.
    /// </summary>
    /// <remarks>
    /// This profile defines mappings for updating sale items and transforming 
    /// domain entities into response/result objects.
    /// </remarks>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the AutoMapper mappings for updating sale items.
        /// </summary>
        public UpdateSaleProfile()
        {
            // Map from domain entity SaleItem to UpdateSaleItemResult
            CreateMap<SaleItem, UpdateSaleItemResult>();

            // Map from UpdateSaleItemCommand to SaleItem for updating data
            CreateMap<UpdateSaleItemCommand, SaleItem>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled));
        }
    }
}

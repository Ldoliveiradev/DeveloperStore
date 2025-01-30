using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Defines the mapping profile for sale item creation between API requests, commands, and responses.
    /// </summary>
    public class CreateSaleItemProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for creating a sale item.
        /// </summary>
        public CreateSaleItemProfile()
        {
            /// <summary>
            /// Maps the API request model to the application command model.
            /// </summary>
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();

            /// <summary>
            /// Maps the application result model to the API response model.
            /// </summary>
            CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
        }
    }
}

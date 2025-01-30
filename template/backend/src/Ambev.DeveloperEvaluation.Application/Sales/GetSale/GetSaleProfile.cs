using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between <see cref="Sale"/> entity and <see cref="GetSaleResult"/>.
    /// </summary>
    /// <remarks>
    /// This AutoMapper profile configures mappings used during sale retrieval operations.
    /// </remarks>
    public class GetSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for retrieving a sale.
        /// </summary>
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleResult>();
        }
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between <see cref="Sale"/> entity and <see cref="UpdateSaleResult"/>.
    /// </summary>
    /// <remarks>
    /// This AutoMapper profile configures mappings used during sale update operations.
    /// </remarks>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for updating a sale.
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<Sale, UpdateSaleResult>();
        }
    }
}

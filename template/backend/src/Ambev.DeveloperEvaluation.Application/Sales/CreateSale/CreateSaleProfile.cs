using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between <see cref="CreateSaleCommand"/> and <see cref="Sale"/> entity,
    /// as well as from <see cref="Sale"/> to <see cref="CreateSaleResult"/>.
    /// </summary>
    /// <remarks>
    /// This AutoMapper profile configures mappings used during the sale creation process.
    /// </remarks>
    public class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for creating a sale.
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
               .ConstructUsing(cmd => new Sale(cmd.SaleDate, cmd.Branch, cmd.CustomerId));

            CreateMap<Sale, CreateSaleResult>();
        }
    }
}

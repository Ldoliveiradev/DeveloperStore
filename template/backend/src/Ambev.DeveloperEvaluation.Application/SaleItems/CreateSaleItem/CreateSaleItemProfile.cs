using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Profile for mapping between <see cref="CreateSaleItemCommand"/> and <see cref="SaleItem"/>,
    /// as well as between <see cref="SaleItem"/> and <see cref="CreateSaleItemResult"/>.
    /// </summary>
    public class CreateSaleItemProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the CreateSaleItem operation.
        /// </summary>
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemCommand, SaleItem>()
               .ConstructUsing(cmd => new SaleItem(cmd.ProductName, cmd.Quantity, cmd.UnitPrice));

            CreateMap<SaleItem, CreateSaleItemResult>();
        }
    }
}

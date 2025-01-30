using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleItemRepository
    {
        Task<SaleItem> CreateAsync(SaleItem sale, CancellationToken cancellationToken = default);
        Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Guid id, Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

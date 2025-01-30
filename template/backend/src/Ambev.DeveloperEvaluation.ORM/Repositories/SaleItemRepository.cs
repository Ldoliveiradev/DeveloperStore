using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleItemRepository : ISaleItemRepository
    {
        readonly DefaultContext _context;

        public SaleItemRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            await _context.SaleItems.AddAsync(saleItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var saleItem = await GetByIdAsync(id, cancellationToken);
            if (saleItem == null)
                return false;

            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, Sale sale, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<Sale> CreateItemAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }


        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(s => s.SaleItems).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, Sale sale, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

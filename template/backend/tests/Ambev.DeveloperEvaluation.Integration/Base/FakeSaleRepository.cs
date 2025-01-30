using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Integration.Base
{
    public class FakeSaleRepository : IRepository<Sale>
    {
        private readonly List<Sale> _sales = new();

        public Task<Sale> AddAsync(Sale entity)
        {
            entity.Id = Guid.NewGuid();
            _sales.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<Sale?> GetByIdAsync(Guid id)
        {
            var sale = _sales.SingleOrDefault(s => s.Id == id);
            return Task.FromResult(sale);
        }

        public Task UpdateAsync(Sale entity)
        {
            var existing = _sales.SingleOrDefault(s => s.Id == entity.Id);
            if (existing != null)
            {
                _sales.Remove(existing);
                _sales.Add(entity);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Sale entity)
        {
            _sales.Remove(entity);
            return Task.CompletedTask;
        }
    }
}

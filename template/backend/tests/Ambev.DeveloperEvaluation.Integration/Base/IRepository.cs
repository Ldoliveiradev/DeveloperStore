namespace Ambev.DeveloperEvaluation.Integration.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}

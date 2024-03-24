 using Core.Domain.Entities;

namespace Core.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> DeleteAsync(int productId);
        IAsyncEnumerable<Product> GetAllAsync(int skip = 0, int take = 25);
        Task<Product> GetId(int productId);

    }
}

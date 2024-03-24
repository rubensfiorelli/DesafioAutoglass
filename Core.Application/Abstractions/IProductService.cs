using Core.Application.DTOs;

namespace Core.Domain.Abstractions
{
    public interface IProductService
    {
        Task<bool> Add(ProductRequest productRequest);
        Task<bool> Update(int productId, ProductRequest request);
        Task<ProductResponse> Delete(int productId);
        IAsyncEnumerable<ProductResponse> GetAll();
        Task<ProductResponse> GetId(int productId);
    }
}

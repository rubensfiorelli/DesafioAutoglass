using Core.Application.DTOs;
using Core.Application.Results;
using Core.Domain.Abstractions;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository) => _repository = repository;

        public async Task<bool> Add(ProductRequest productRequest)
        {
            Result result;
            var entity = (Product)productRequest;
            if (entity.IsValid())
            {
                try
                {
                    entity.Active = true;
                    await _repository.AddAsync(entity);
                    result = new Result(201, $"{entity.Description} successfully created", true, entity);
                    return result.Success;
                }
                catch (DbUpdateException)
                {
                    _ = new Result(400, $"Fail add product", false, productRequest);
                }

            }
            result = new Result(400, $"Fail Server Error", false, entity);
            result.SetNotifications(result.Notifications.ToList());            
            return result.Success;
           
            
        }

        public async Task<ProductResponse> Delete(int productId)
        {
            var existing = await _repository.DeleteAsync(productId);

            return existing;

        }

        public async IAsyncEnumerable<ProductResponse> GetAll()
        {          

            var getAll = _repository.GetAllAsync();

            await foreach (var item in getAll)
            {
                yield return item;
            }
        }

        public async Task<ProductResponse> GetId(int productId)
        {
            var getId = await _repository.GetId(productId);
            if (getId is null)
                return null;

            return (ProductResponse)getId;
        }

     
        public async Task<bool> Update(int productId, ProductRequest model)
        {
            Result result;
            var entity = (Product)model;
            await _repository.GetId(productId);
            if (entity.IsValid())
            {
                try
                {
                    entity.SetProduct(model.Description,
                                      model.Manufacturing,
                                      model.Validate,
                                      model.SupplierCode,
                                      model.SupplierDescription,
                                      model.SupplierCNPJ);

                    await _repository.UpdateAsync(entity);
                    result = new Result(200, $"{entity.Id} successfully updated", true, entity);
                    return result.Success;

                }
                catch (Exception)
                {
                    _ = new Result(400, $"Fail add product", false, model);
                }
                result = new Result(400, $"Fail Server Error", false, entity);
                result.SetNotifications(result.Notifications.ToList());
                return result.Success;
            }
            return false;            

        }
    }
}

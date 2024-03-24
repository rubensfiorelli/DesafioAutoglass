using Core.Application.DTOs;
using Core.Domain.Abstractions;
using Core.Domain.Entities;
using Infra.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(Product product)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.AddAsync(product);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync().ConfigureAwait(false);

            }
            catch (DbUpdateException)
            {

                await transaction.RollbackAsync();
            }
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var inactivate = await _context.Products
                    .AsTracking()
                    .FirstOrDefaultAsync(n => n.Id.Equals(productId));

                if (inactivate is null)
                    return Product.NULL;

                inactivate.InactivateProduct();
                await _context.SaveChangesAsync();

                await transaction.CommitAsync().ConfigureAwait(false);

                return inactivate;
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async IAsyncEnumerable<Product> GetAllAsync(int skip = 0, int take = 25)
        {

            var products = _context.Products
                .Where(n => !n.Inactive)
                .Skip(skip)
                .Take(take)
                .Select(item => new Product
                {
                    Id = item.Id,
                    Description = item.Description,
                    Active = item.Active,
                    Manufacturing = item.Manufacturing,
                    Validate = item.Validate,
                    SupplierCode = item.SupplierCode,
                    SupplierDescription = item.SupplierDescription,
                    SupplierCNPJ = item.SupplierCNPJ

                }).AsAsyncEnumerable();

            await foreach (var item in products)
            {
                yield return item;
            }
        }

        public async Task<Product> GetId(int productId)
        {
            var existing = await _context.Products
                .AsTracking()
                .SingleOrDefaultAsync(n => n.Id.Equals(productId));

            if (existing is null)
                return Product.NULL;

            return existing;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.Products
                .AsTracking()
                .SingleOrDefaultAsync(n => n.Id.Equals(product.Id));

                if (existing is null)
                    return Product.NULL;

                existing.SetProduct(product.Description,
                                      product.Manufacturing,
                                      product.Validate,
                                      product.SupplierCode,
                                      product.SupplierDescription,
                                      product.SupplierCNPJ);

                _context.Update(existing);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync().ConfigureAwait(false);

                return existing;
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();
                throw;
            }


        }
    }
}

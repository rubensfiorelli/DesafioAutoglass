using Core.Domain.Entities;

namespace Core.Application.DTOs
{
    public readonly record struct ProductResponse
    {
        public int Id { get; init; }
        public string? Description { get; init; }
        public bool Active { get; init; }
        public DateTime? Manufacturing { get; init; }
        public DateTime? Validate { get; init; }
        public int SupplierCode { get; init; }
        public string? SupplierDescription { get; init; }
        public string? SupplierCNPJ { get; init; }

        public static implicit operator ProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Description = product.Description,
                Active = product.Active,
                Manufacturing = product.Manufacturing,
                Validate = product.Validate,
                SupplierCode = product.SupplierCode,
                SupplierDescription = product.SupplierDescription,
                SupplierCNPJ = product.SupplierCNPJ

            };
        }
    }
}

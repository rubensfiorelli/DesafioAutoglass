using Core.Domain.Entities;

namespace Core.Application.DTOs
{
    public readonly record struct ProductRequest
    {
        public string Description { get; init; }
        public bool Active { get; init; }
        public DateTime Manufacturing { get; init; }
        public DateTime Validate { get; init; }
        public int SupplierCode { get; init; }
        public string SupplierDescription { get; init; }
        public string SupplierCNPJ { get; init; }

        public static implicit operator Product(ProductRequest request)
        {
            return new()
            {
                Description = request.Description,
                Active = request.Active,
                Manufacturing = request.Manufacturing,
                Validate = request.Validate,
                SupplierCode = request.SupplierCode,
                SupplierDescription = request.SupplierDescription,
                SupplierCNPJ = request.SupplierCNPJ
            };

        }
    }
}

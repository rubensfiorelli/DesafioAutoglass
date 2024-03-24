using Core.Domain.Contracts;
using Core.Domain.Notifications;
using Core.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Product : IValidation, IContract
    {
        private List<Notification> _notifications;

        public int Id { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = true;
        public bool Inactive { get; set; }
        public DateTime Manufacturing { get; set; }
        public DateTime Validate { get; set; }
        public int SupplierCode { get; set; }
        public string? SupplierDescription { get; set; }
        public string? SupplierCNPJ { get; set; }
        
        [NotMapped]
        public IReadOnlyCollection<Notification> Notifications => _notifications;


        protected void SetNotifications(List<Notification> notifications)
        {
            _notifications = notifications;
        }
        public void InactivateProduct() => Inactive = true;
        public void SetProduct(string? description,
                                 DateTime manufacturing,
                                 DateTime validate,
                                 int supplierCode,
                                 string? supplierDescription,
                                 string? supplierCNPJ)
        {
            Description = description;
            Manufacturing = manufacturing;
            Validate = validate;
            SupplierCode = supplierCode;
            SupplierDescription = supplierDescription;
            SupplierCNPJ = supplierCNPJ;
        }

        public bool IsValid()
        {
            var contracts = new ContractValidations<Product>()
                .ValidManufactureIsOk(Manufacturing, Validate, "Invalid manufacturing date", nameof(Manufacturing));

            return contracts.IsValid();
        }

        public static readonly Product NULL = new NullProduct();
        private class NullProduct : Product { }

    }
}

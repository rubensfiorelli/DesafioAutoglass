using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    internal class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250);

            builder.Property(x => x.SupplierCode)
              .IsRequired()
              .HasColumnType("VARCHAR")
              .HasMaxLength(32);

            builder.Property(x => x.SupplierDescription)
             .IsRequired()
             .HasColumnType("NVARCHAR")
             .HasMaxLength(200);

            builder.Property(x => x.SupplierCNPJ)
             .IsRequired()
             .HasColumnType("VARCHAR")
             .HasMaxLength(14);

            builder
              .Property(c => c.Manufacturing)
              .HasDefaultValueSql("GETDATE()")
              .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore
              .Metadata.PropertySaveBehavior.Ignore);

        }
    }
}

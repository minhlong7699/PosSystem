using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasData
            (
            new Supplier
            {
                SupplierId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991855"),
                SupplierName = "Công ty CP",
                Address = "Tphcm",
                Email = "cp@gmail.com",
                PhoneNumber = 090123456,
                CreatedAt = DateTime.Now,
                CreatedBy = "Admin",
                IsActive = true,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "Admin"
            }
            );
        }
    }
}

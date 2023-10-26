using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
                (
                new Product
                {
                    ProductId = new Guid("00000000-0000-0000-0000-000000000001"),
                    ProductName = "Cánh gà",
                    ProductCode = "GR01",
                    ProductDescription = "Miêu tả",
                    ProductPrice = 25000,
                    ProductPriceAfterDiscount = 25000,
                    Image = "test",
                    CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                    StockQuantity = 1,
                    SupplierId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991855"),
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

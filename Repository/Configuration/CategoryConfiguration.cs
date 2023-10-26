using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                new Category
                {
                    CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    CategoryName = "Gà Rán",
                    CategoryDescription = "Description",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Admin",
                    IsActive = true,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "Admin"
                },
                new Category
                {
                    CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                    CategoryName = "Hambuger",
                    CategoryDescription = "Description",
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

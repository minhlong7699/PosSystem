using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateProduct(Guid categoryId, Product product)
        {
            product.CategoryId = categoryId;
            Create(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges)
        {
            return await FindByConditon(
                e => e.CategoryId.Equals(categoryId),
                trackChanges).OrderBy(e => e.ProductName)
                .Skip((productParameters.pageNumber - 1) * productParameters.pageSize)
                .Take(productParameters.pageSize)
                .ToListAsync();
        }


        public async Task<Product> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges)
        {
            return await FindByConditon(e => e.CategoryId.Equals(categoryId) && e.ProductId.Equals(productId), trackChanges).SingleOrDefaultAsync();
        }

    }
}

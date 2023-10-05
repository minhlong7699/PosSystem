using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
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

        public async Task<PagedList<Product>> GetAllProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges)
        {
            var products = await FindByConditon(e => e.CategoryId.Equals(categoryId),trackChanges)
                .FilterProduct(productParameters.MinPrice, productParameters.MaxPrice, productParameters.IsActive)
                .SearchProduct(productParameters.SearchTerm)
                .SortProduct(productParameters.orderBy)
                .ToListAsync();

            return PagedList<Product>.ToPagedList(products, productParameters.pageNumber, productParameters.pageSize);
        }


        public async Task<Product> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges)
        {
            return await FindByConditon(e => e.CategoryId.Equals(categoryId) && e.ProductId.Equals(productId), trackChanges).SingleOrDefaultAsync();
        }

    }
}

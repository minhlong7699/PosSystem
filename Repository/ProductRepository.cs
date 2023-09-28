using Contract;
using Entity.Models;
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

        public void CreateProduct(Guid CategoryId, Product product)
        {
            product.CategoryId = CategoryId;
            Create(product);
        }

        public IEnumerable<Product> GetAllProducts(Guid categoryId, bool trackChanges)
        {
            return FindByConditon(e => e.CategoryId.Equals(categoryId), trackChanges).OrderBy(e => e.ProductName).ToList();
        }

        public Product GetProduct(Guid categoryId, Guid productId, bool trackChanges)
        {
            return FindByConditon(e => e.CategoryId.Equals(categoryId) && e.ProductId.Equals(productId), trackChanges).SingleOrDefault();
        }

    }
}

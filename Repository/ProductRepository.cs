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

        public IEnumerable<Product> GetAllProducts(Guid categoryId, bool trackChanges)
        {
            return FindByConditon(e => e.CategoryId.Equals(categoryId), trackChanges).OrderBy(e => e.ProductName).ToList();
        }
    }
}

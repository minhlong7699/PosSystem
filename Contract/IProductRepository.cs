using Entity.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(Guid categoryId, ProductParameters productParameters , bool trackChanges);
        Task<Product> GetProductAsync( Guid categoryId ,Guid productId, bool trackChanges);
        void CreateProduct(Guid categoryId, Product product);
    }
}

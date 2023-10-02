using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts(Guid categoryId, bool trackChanges);

        ProductDto GetProduct(Guid categoryId, Guid productId, bool trackChanges);

        ProductDto CreateProduct(Guid categoryId, ProductUpdateCreateDto productCreate, bool trackChanges);

        void UpdateProduct(Guid categoryId, Guid productId, ProductUpdateCreateDto productUpdate, bool categoryTrachkChanges, bool productTrachChages);
    }
}

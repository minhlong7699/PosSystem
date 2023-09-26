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
    }
}

using Shared.DataTransferObjects.Supplier;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface ISupplierService
    {
        Task<(IEnumerable<SupllierDto> suppliers, MetaData metadata)> GetAllSuppliersAsync(SupplierParameter supplierParameter,bool trackChanges);

        Task<SupllierDto> GetSupplierAsync(Guid supplierId, bool trackChanges);

        Task<SupllierDto> CreateSupplierAsync(SupplierCreateUpdateDto supplierCreate);

        Task UpdateSupplierAsync(Guid supplierId, SupplierCreateUpdateDto supplierUpdate, bool trackChanges);

        Task DeleteSupplierAsync(Guid supplierId, bool trackChanges);
    }
}

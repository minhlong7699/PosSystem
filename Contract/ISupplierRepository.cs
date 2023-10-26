using Entity.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface ISupplierRepository
    {
        Task<PagedList<Supplier>> GetAllSuppliersAsync(SupplierParameter supplierParameter ,bool trackChanges);

        Task<Supplier> GetSupplierAsync(Guid? supplierId, bool trackChanges);

        void CreateSupplier(Supplier supplier);

        void DeleteSupplier(Supplier supplier);
    }
}

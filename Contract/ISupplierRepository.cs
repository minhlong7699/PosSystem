using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync(bool trackChanges);

        Task<Supplier> GetSupplierAsync(Guid? supplierId, bool trackChanges);

        void CreateSupplier(Supplier supplier);

        void DeleteSupplier(Supplier supplier);
    }
}

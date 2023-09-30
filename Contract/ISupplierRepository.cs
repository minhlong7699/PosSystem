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
        IEnumerable<Supplier> GetAllSuppliers(bool trackChanges);

        Supplier GetSupplier(Guid? supplierId, bool trackChanges);
    }
}

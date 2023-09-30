using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface ISupplierService
    {
        IEnumerable<SupllierDto> GetAllSuppliers(bool trackChanges);

        SupllierDto GetSupplier(Guid supplierId, bool trackChanges);
    }
}

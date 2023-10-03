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
        Task<IEnumerable<SupllierDto>> GetAllSuppliersAsync(bool trackChanges);

        Task<SupllierDto> GetSupplierAsync(Guid supplierId, bool trackChanges);
    }
}

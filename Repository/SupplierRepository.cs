using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(x => x.SupplierName)
                .ToListAsync();
        }

        public async Task<Supplier> GetSupplierAsync(Guid? supplierId, bool trackChanges)
        {
            return await FindByConditon(x => x.SupplierId.Equals(supplierId), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}

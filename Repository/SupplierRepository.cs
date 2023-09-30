using Contract;
using Entity.Models;
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

        public IEnumerable<Supplier> GetAllSuppliers(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(x => x.SupplierName)
                .ToList();
        }

        public Supplier GetSupplier(Guid? supplierId, bool trackChanges)
        {
            return FindByConditon(x => x.SupplierId.Equals(supplierId), trackChanges)
                .SingleOrDefault();
        }
    }
}

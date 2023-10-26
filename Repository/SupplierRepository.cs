using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
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

        public void CreateSupplier(Supplier supplier)
        {
            Create(supplier);
        }

        public void DeleteSupplier(Supplier supplier)
        {
            Delete(supplier);
        }

        public async Task<PagedList<Supplier>> GetAllSuppliersAsync(SupplierParameter supplierParameter ,bool trackChanges)
        {
            var supplier = await FindAll(trackChanges)
                .SearchSupplier(supplierParameter.SearchTerm)
                .SortSupplier(supplierParameter.orderBy)
                .ToListAsync();
            return PagedList<Supplier>.ToPagedList(supplier, supplierParameter.pageNumber, supplierParameter.pageSize);
        }

        public async Task<Supplier> GetSupplierAsync(Guid? supplierId, bool trackChanges)
        {
            return await FindByConditon(x => x.SupplierId.Equals(supplierId), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}

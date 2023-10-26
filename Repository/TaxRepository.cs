using Contract;
using Entity.Exceptions;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TaxRepository : RepositoryBase<Tax>, ITaxRepository
    {
        public TaxRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateTax(Tax tax)
        {
            Create(tax);
        }

        public void DeleteTax(Tax tax)
        {
            Delete(tax);
        }

        public async Task<PagedList<Tax>> GetAllTaxesAsync(TaxParameters taxParameters, bool trackChanges)
        {
            var taxes = await FindAll(trackChanges).ToListAsync();
            return PagedList<Tax>.ToPagedList(taxes, taxParameters.pageNumber, taxParameters.pageSize);
        }

        public async Task<Tax> GetTaxAsync(Guid taxId, bool trackChanges)
        {
            return await FindByConditon(x => x.TaxId.Equals(taxId), trackChanges).SingleOrDefaultAsync();
        }
    }
}

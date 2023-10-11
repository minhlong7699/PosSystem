using Entity.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface ITaxRepository
    {
        Task<PagedList<Tax>> GetAllTaxesAsync(TaxParameters taxParameters,bool trackChanges);

        Task<Tax> GetTaxAsync(Guid taxId, bool trackChanges);

        void CreateTax(Tax tax);
    }
}

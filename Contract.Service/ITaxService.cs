using Entity.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface ITaxService
    {
        Task<(IEnumerable<TaxDto> taxes, MetaData metadata)> GetAllTaxesAsync(TaxParameters taxParameters, bool trackChanges);

        Task<TaxDto> GetTaxAsync(Guid taxId, bool trackChanges);

        Task<TaxDto> CreateTax(TaxCreateUpdateDto taxCreate);
    }
}

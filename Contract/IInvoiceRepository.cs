using Entity.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IInvoiceRepository
    {
        Task<PagedList<Invoice>> GetAllInvoicesAsync(InvoiceParameter invoiceParameter ,bool trackChanges);

        Task<Invoice> GetInvoiceAsync(Guid invoiceId, bool trackChanges);

        void CreateInvoice(Invoice invoice, bool trackChanges);

    }
}

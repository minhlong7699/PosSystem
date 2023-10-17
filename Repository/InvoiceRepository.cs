using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateInvoice(Invoice invoice, bool trackChanges)
        {
            Create(invoice);
        }

        public async Task<PagedList<Invoice>> GetAllInvoicesAsync(InvoiceParameter invoiceParameter, bool trackChanges)
        {
            var invoices = await FindAll(trackChanges).ToListAsync();
            return PagedList<Invoice>.ToPagedList(invoices, invoiceParameter.pageNumber, invoiceParameter.pageSize);          
        }

        public async Task<Invoice> GetInvoiceAsync(Guid invoiceId, bool trackChanges)
        {
            var invoice = await FindByConditon(x => x.InvoiceId.Equals(invoiceId), trackChanges).SingleOrDefaultAsync();
            return invoice;
        }


    }
}

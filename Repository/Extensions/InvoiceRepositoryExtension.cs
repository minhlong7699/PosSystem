using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class InvoiceRepositoryExtension
    {
        public static IQueryable<Invoice> FilterInvoice(this IQueryable<Invoice> invoices, DateTime startDate, DateTime endDate)
        {
            return invoices.Where(x => (x.InvoiceDate >= startDate && x.InvoiceDate <= endDate));
        }
    }
}

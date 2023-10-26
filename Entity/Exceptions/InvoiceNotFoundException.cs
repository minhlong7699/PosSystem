using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class InvoiceNotFoundException : NotFoundException
    {
        public InvoiceNotFoundException(Guid invoiceId) : base($"Invoice with id: {invoiceId} doesn't exist in the database.")
        {
        }
    }
}

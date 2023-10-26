using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class SupplierNotFoundException : NotFoundException
    {
        public SupplierNotFoundException(Guid? supplierid) : base($"The supplier with id: {supplierid} doesn't exist in the database.")
        {
        }
    }
}

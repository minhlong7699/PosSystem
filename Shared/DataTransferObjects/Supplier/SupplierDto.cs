using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Supplier
{
    public record SupllierDto(Guid SupplierId, string SupplierName, string Email, int PhoneNumber, string Address);
}

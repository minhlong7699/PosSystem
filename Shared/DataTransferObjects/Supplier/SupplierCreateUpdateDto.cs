using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Supplier
{
    public record SupplierCreateUpdateDto(string SupplierName, string Email, int PhoneNumber, string Address);
}

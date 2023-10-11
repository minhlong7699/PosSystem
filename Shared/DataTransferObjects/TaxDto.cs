using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record TaxDto(Guid TaxId, string TaxName, float TaxRate, bool IsActive);
}

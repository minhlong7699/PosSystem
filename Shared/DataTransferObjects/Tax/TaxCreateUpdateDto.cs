using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Tax
{
    public record TaxCreateUpdateDto(string TaxName, float TaxRate, bool IsActive);
}

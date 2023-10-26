using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Payment
{
    public record PaymentUpdateCreateDto(string PaymentType, bool IsActive);
}

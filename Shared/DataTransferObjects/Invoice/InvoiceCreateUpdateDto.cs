using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Invoice
{
    public record InvoiceCreateUpdateDto(Guid OrderId, string CustomerName, string? ShippingAddress);

    public record InvoiceUpdateDto(string? CustomerName, string? ShippingAddress, string? Status);
}

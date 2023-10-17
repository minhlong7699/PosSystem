﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record InvoiceCreateUpdateDto(Guid OrderId, string CustomerName, string? ShippingAddress);
}
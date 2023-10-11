using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class PaymentNotFoundException : NotFoundException
    {
        public PaymentNotFoundException(Guid paymentId) : base($"Payment with id: {paymentId} doesn't exist in the database.")
        {
        }
    }
}

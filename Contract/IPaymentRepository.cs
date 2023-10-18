using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges);

        Task<Payment> GetPaymentAsync(Guid paymentId, bool trackChanges);

        void CreatePayment(Payment payment);

        void DeletePayment(Payment payment);
    }
}

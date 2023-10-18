using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreatePayment(Payment payment)
        {
            Create(payment);
        }

        public void DeletePayment(Payment payment)
        {
            Delete(payment);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges)
        {
            var payments = await FindAll(trackChanges).ToListAsync();
            return payments;
        }

        public async Task<Payment> GetPaymentAsync(Guid paymentId, bool trackChanges)
        {
            var payment = await FindByConditon(x => x.PaymentId.Equals(paymentId), trackChanges).SingleOrDefaultAsync();
            return payment;
        }
    }
}

using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentAsync(bool trackChanges);

        Task<PaymentDto> GetPaymentAsync(Guid paymentId, bool trackChanges);

        Task<PaymentDto> CreatePaymentAsync(PaymentUpdateCreateDto paymentCreate);

        Task UpdatePaymentAsync(Guid paymentId,PaymentUpdateCreateDto paymentUpdate, bool trackChanges);

        Task DeletePaymentAsync(Guid paymentId, bool trackChanges);
    }
}

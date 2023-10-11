using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class PaymentService : IPaymentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PaymentService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // Create Payment
        public async Task<PaymentDto> CreatePaymenAsync(PaymentUpdateCreateDto paymentCreate)
        {
            var paymentEntity = _mapper.Map<Payment>(paymentCreate);
            paymentEntity.CreatedAt = DateTime.Now;
            paymentEntity.CreatedBy = "Admin";
            paymentEntity.UpdatedAt = DateTime.Now;
            paymentEntity.UpdatedBy = "Admin";
            _repository.PaymentRepository.CreatePayment(paymentEntity);
            await _repository.SaveAsync();

            var paymentDto = _mapper.Map<PaymentDto>(paymentEntity);
            return paymentDto;
        }

        // GetAllPayment
        public async Task<IEnumerable<PaymentDto>> GetAllPaymentAsync(bool trackChanges)
        {
            var payment = await _repository.PaymentRepository.GetAllPaymentsAsync(trackChanges);
            var paymentDto = _mapper.Map<IEnumerable<PaymentDto>>(payment);
            return paymentDto;
        }

        // Get Payment By Id
        public async Task<PaymentDto> GetPaymentAsync(Guid paymentId, bool trackChanges)
        {
            var payment = await _repository.PaymentRepository.GetPaymentAsync(paymentId, trackChanges);
            if(payment is null)
            {
                throw new PaymentNotFoundException(paymentId);
            }
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            return paymentDto;
        }
    }
}
